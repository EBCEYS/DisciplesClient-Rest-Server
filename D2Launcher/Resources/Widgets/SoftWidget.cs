using D2Launcher.Tools;
using Disciples2ApiModels.D2ApiModels;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.Json;

namespace D2Launcher.Resources.Widgets
{
    public partial class SoftWidget : BaseD2Widget
    {
        private List<ModInfo> AllowedSofts = new();
        public SoftWidget() : base()
        {
            InitializeComponent();
            OnShowingWidget += UpdateAllowedSoftList;
            OnShowingWidget += UpdateInstalledSoftList;
        }

        private void UpdateAllowedSoftList()
        {
            AllowedSofts = Program.HttpSender.GetSoftInfo().ToList() ?? AllowedSofts;
            FillAllowedSoftComboBox();
        }

        private void FillAllowedSoftComboBox()
        {
            AllowedSoftComboBox.Text = string.Empty;
            AllowedSoftComboBox.Items.Clear();
            AllowedSofts.Where(x => !ModsInfoStorage.IsContains(x)).Select(s => s.Name).ToList().ForEach(name =>
            {
                AllowedSoftComboBox.Items.Add(name);
            });
        }

        private void UpdateInstalledSoftList()
        {
            List<ModInfo> installedSoft = Program.HttpSender.GetSoftInfo().Where(s => ModsInfoStorage.IsContains(s)).ToList();
            FillInstalledSoftComboBox(installedSoft);
        }

        private void FillInstalledSoftComboBox(List<ModInfo> softs)
        {
            InstalledSoftsComboBox.Text = string.Empty;
            InstalledSoftsComboBox.Items.Clear();
            softs.ForEach(x =>
            {
                InstalledSoftsComboBox.Items.Add(x.Name);
            });
        }

        private void DownloadSoftButton_Click(object sender, EventArgs e)
        {
            DownloadSoft();
        }

        private void DownloadSoft()
        {
            ModInfo? modNullable = GetSelectedSoft();
            if (modNullable is null)
            {
                return;
            }
            ModInfo mod = modNullable ?? throw new Exception("Unexpected exception!");
            if (ModsInfoStorage.IsContains(mod))
            {
                MessageBox.Show("You have already installed this!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string modPath = Program.GetSoftDir(mod.Name);
            if (!Directory.Exists(modPath))
            {
                Directory.CreateDirectory(modPath);
            }
            try
            {
                ProcessInfoLabel.Text = "Downloading...";
                Task.Run(async () =>
                {
                    string downloadedFile = await Program.HttpSender.DownloadModFileAsync(mod.Name, modPath);
                    ZipFile.ExtractToDirectory(downloadedFile, modPath, true);
                    File.Delete(downloadedFile);
                }).Wait();
                ProcessInfoLabel.Text = "Creating soft info file...";
                Task.Run(async () => await CreateModInfoFileAsync(mod, modPath)).Wait();
                ProcessInfoLabel.Text = "Writing soft file info...";
                ModsInfoStorage.TryAdd(new(mod, modPath));
                ProcessInfoLabel.Text = "Done!";
            }
            catch (Exception ex)
            {
                ShowException(ex);
                ProcessInfoLabel.Text = "Error!";
            }
            FillAllowedSoftComboBox();
            UpdateInstalledSoftList();
        }

        public static async Task CreateModInfoFileAsync(ModInfo mod, string modPath)
        {
            string modInfoFile = Path.Combine(modPath, "modInfo.json");
            string json = JsonSerializer.Serialize(mod, Program.SerializerOptions);
            await File.WriteAllTextAsync(modInfoFile, json);
        }

        private ModInfo? GetSelectedSoft()
        {
            string selectedMod = AllowedSoftComboBox.Text;
            if (string.IsNullOrEmpty(selectedMod) || !AllowedSofts.Exists(x => x.Name == selectedMod))
            {
                MessageBox.Show("Please select item in list!");
                return null;
            }
            return AllowedSofts.First(x => x.Name == selectedMod);

        }
        private static void ShowException(Exception ex)
        {
            MessageBox.Show($"Exception! {ex.Message}{Environment.NewLine}{ex.StackTrace}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OpenSoftDirButton_Click(object sender, EventArgs e)
        {
            InstalledModInfo soft = GetSelectedInstalledSoft();
            if (soft is null)
            {
                MessageBox.Show("Can not find this software in storage!");
                return;
            }
            try
            {
                Task.Run(() => Process.Start("explorer.exe", soft.Path));
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
        }

        private InstalledModInfo? GetSelectedInstalledSoft()
        {
            string selectedMod = InstalledSoftsComboBox.Text;
            if (string.IsNullOrEmpty(selectedMod) || !ModsInfoStorage.InstalledModsInfo.Exists(x => x.ModInfo.Name == selectedMod))
            {
                MessageBox.Show("Please select item in list!");
                return null;
            }
            return ModsInfoStorage.InstalledModsInfo.First(x => x.ModInfo.Name == selectedMod);

        }

        private void RemoveSoftButton_Click(object sender, EventArgs e)
        {
            InstalledModInfo soft = GetSelectedInstalledSoft();
            if (soft is null)
            {
                return;
            }
            DialogResult dResult = MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dResult != DialogResult.Yes)
            {
                return;
            }
            try
            {
                ModsInfoStorage.RemoveMod(soft);
                UpdateInstalledSoftList();
                UpdateAllowedSoftList();
                MessageBox.Show("Soft deleted successfuly!");
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
        }
    }
}
