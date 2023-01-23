using D2Launcher.Tools;
using Disciples2ApiModels.D2ApiModels;
using System.IO.Compression;
using System.Text.Json;

namespace D2Launcher.Resources.Widgets
{
    public partial class ModsListWidget : BaseD2Widget
    {
        public ModsListWidget() : base()
        {
            InitializeComponent();
            OnShowingWidget += FillModsComboBox;
        }

        private List<ModInfo> modsInfo;

        private void ModsListWidget_Load(object sender, EventArgs e)
        {
            if (Program.Url == null)
            {
                return;
            }
            FillModsComboBox();
        }

        private void FillModsComboBox()
        {
            try
            {
                ModsListComboBox.Text = string.Empty;
                ModsListComboBox.Items.Clear();
                ProcessInfoLabel.Text = "Getting mod list...";
                modsInfo = Program.HttpSender.GetModsInfo()?.ToList() ?? modsInfo;
                foreach (ModInfo mod in modsInfo)
                {
                    if (!ModsInfoStorage.IsContains(mod))
                    {
                        ModsListComboBox.Items.Add(mod.Name);
                    }
                }
                if (ModsListComboBox.Items.Count > 0)
                {
                    ModsListComboBox.SelectedItem = ModsListComboBox.Items[0];
                }
                ProcessInfoLabel.Text = "Done!";
            }
            catch (Exception ex)
            {
                ShowException(ex);
                ProcessInfoLabel.Text = "Error!";
            }
        }

        private static void ShowException(Exception ex)
        {
            MessageBox.Show($"Exception! {ex.Message}{Environment.NewLine}{ex.StackTrace}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            ModInfo? info = GetSelectedMod();
            if (info is null)
            {
                return;
            }
            MessageBox.Show($"Name: {info?.Name}{Environment.NewLine}Last version: {info?.Version}{Environment.NewLine}Author: {info?.Author}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private ModInfo? GetSelectedMod()
        {
            string selectedMod = ModsListComboBox.Text;
            if (string.IsNullOrEmpty(selectedMod) || !modsInfo.Exists(x => x.Name == selectedMod))
            {
                MessageBox.Show("Please select item in list!");
                return null;
            }
            return modsInfo.FirstOrDefault(x => x.Name == selectedMod);
        }

        private void UpdateModListButton_Click(object sender, EventArgs e)
        {
            FillModsComboBox();
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            ModInfo? modNullable = GetSelectedMod();
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
            ProcessInfoLabel.Text = "Getting mod directory...";
            string modPath = Program.GetModDir(mod.Name);
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
                ProcessInfoLabel.Text = "Creating mod info file...";
                Task.Run( async () => await CreateModInfoFileAsync(mod, modPath)).Wait();
                ProcessInfoLabel.Text = "Writing mod file info...";
                ModsInfoStorage.TryAdd(new(mod, modPath));
                ProcessInfoLabel.Text = "Done!";
            }
            catch (Exception ex)
            {
                ShowException(ex);
                ProcessInfoLabel.Text = "Error!";
            }
            FillModsComboBox();
        }

        public static async Task CreateModInfoFileAsync(ModInfo mod, string modPath)
        {
            string modInfoFile = Path.Combine(modPath, "modInfo.json");
            string json = JsonSerializer.Serialize(mod, Program.SerializerOptions);
            await File.WriteAllTextAsync(modInfoFile, json);
        }
    }
}
