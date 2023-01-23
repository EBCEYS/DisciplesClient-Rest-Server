using D2Launcher.Tools;
using Disciples2ApiModels.D2ApiModels;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.AccessControl;

namespace D2Launcher.Resources.Widgets
{
    public partial class InstalledD2ModsWidget : BaseD2Widget
    {
        public InstalledD2ModsWidget() : base()
        {
            InitializeComponent();
            OnShowingWidget += UpdateModList;
        }

        private void InstalledD2ModsWidget_Load(object sender, EventArgs e)
        {
            UpdateModList();
        }

        private void UpdateModList()
        {
            InstalledModsComboBox.Text = string.Empty;
            InstalledModsComboBox.Items.Clear();
            foreach (InstalledModInfo mi in ModsInfoStorage.InstalledModsInfo)
            {
                InstalledModsComboBox.Items.Add(mi.ModInfo.Name);
            }
            if (InstalledModsComboBox.Items.Count > 0)
            {
                InstalledModsComboBox.Text = InstalledModsComboBox.Items[0].ToString();
            }
        }

        private InstalledModInfo GetSelectedMod()
        {
            string selectedMod = InstalledModsComboBox.Text;
            InstalledModInfo mod = ModsInfoStorage.InstalledModsInfo.FirstOrDefault(x => x.ModInfo.Name == selectedMod);
            return mod ?? null;
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            InstalledModInfo mod = GetSelectedMod();
            if (mod == null)
            {
                MessageBox.Show("Please select mod from list!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DirectoryInfo modDirInfo = new(mod.Path);
            if (!modDirInfo.Exists)
            {
                MessageBox.Show("Can not find directory!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FileInfo gameFile = modDirInfo.GetFiles("Discipl2.exe").FirstOrDefault();
            if (gameFile == null)
            {
                MessageBox.Show("Can not find 'Discipl2.exe' file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start(gameFile.FullName);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            InstalledModInfo mod = GetSelectedMod();
            if (mod == null)
            {
                MessageBox.Show("Please select mod from list!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool removeResult = ModsInfoStorage.RemoveMod(mod);
            string resultMsg = removeResult ? "OK" : "ERROR";
            UpdateModList();
            MessageBox.Show($"Remove result is: {resultMsg}!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CheckUpdatesButton_Click(object sender, EventArgs e)
        {
            ModInfo[] mods = Program.HttpSender.GetModsInfo();
            List<InstalledModInfo> modsToUpdate = new();
            foreach (ModInfo mod in mods)
            {
                InstalledModInfo modInfo = ModsInfoStorage.InstalledModsInfo.FirstOrDefault(x => x.ModInfo.Name == mod.Name && x.ModInfo.Version != mod.Version);
                if (modInfo != null)
                {
                    modsToUpdate.Add(modInfo);
                }
            }
            if (!modsToUpdate.Any()) 
            {
                MessageBox.Show("There's no updates!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult dr = MessageBox.Show($"Mods: {string.Join(", ", modsToUpdate.Select(x => x.ModInfo.Name))} ready to update.{Environment.NewLine}Do you want to update now?{Environment.NewLine}Mod directory will be removed!", "Mods to update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
            {
                return;
            }
            UpdateMods(modsToUpdate.ToArray());
        }

        private static void UpdateMods(params InstalledModInfo[] mods)
        {
            foreach(InstalledModInfo mod in mods)
            {
                DirectoryInfo dirInfo = new(mod.Path);
                try
                {
                    if (dirInfo.Exists)
                    {
                        dirInfo.Delete(true);
                    }
                    dirInfo.Create();
                    string modPath = dirInfo.FullName;
                    Task.Run(async () =>
                    {
                        string downloadedFile = await Program.HttpSender.DownloadModFileAsync(mod.ModInfo.Name, modPath);
                        ZipFile.ExtractToDirectory(downloadedFile, modPath, true);
                        File.Delete(downloadedFile);
                    }).Wait();
                    Task.Run(async () => await ModsListWidget.CreateModInfoFileAsync(mod.ModInfo, modPath)).Wait();
                    ModsInfoStorage.RemoveMod(mod);
                    ModsInfoStorage.TryAdd(mod);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception! {ex.Message}{Environment.NewLine}{ex.StackTrace}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
