using AdminAndAuthorClient.UserDataStorage;
using AdminAndAuthorClient.UserForms.ModsForms;
using Disciples2ApiModels.ApiModels;
using Disciples2ApiModels.D2ApiModels;

namespace AdminAndAuthorClient.UserForms.MainUserForm
{
    public partial class MainForm : Form
    {
        private ModInfo[] mods;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!UserStorage.IsParamsSet())
            {
                LoginForm loginForm = new();
                loginForm.ShowDialog();
                if (!UserStorage.IsParamsSet())
                {
                    this.Close();
                }
            }
            UpdateModsDataGrid();
        }

        private void UpdateModsDataGrid()
        {
            mods = Program.HttpSender.GetModsInfo() ?? mods;
            if (mods is not null)
            {
                ModsListDataGrid.DataSource = mods;
                ModsListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                CreateModsDirs();
            }
        }

        private void CreateModsDirs()
        {
            foreach(ModInfo mod in mods)
            {
                string path = Path.Combine(Program.BasePath, "archives", mod.Name);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private void FilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string modName = GetSelectedModName();
                FilesForm ff = new(modName);
                ff.ShowDialog();
                UpdateModsDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetSelectedModName()
        {
            int selectedRowsCount = ModsListDataGrid.SelectedRows.Count;
            if (selectedRowsCount <= 0)
            {
                throw new Exception("Select mod in table");
            }
            DataGridViewRow mod = ModsListDataGrid.SelectedRows[0];
            string modName = mod.Cells[0].Value.ToString();
            return modName;
        }

        private void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadModForm form = new();
            form.ShowDialog();
            UpdateModsDataGrid();
        }

        private void UpdateModListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateModsDataGrid();
        }

        private void DownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string modName = GetSelectedModName();
                string modDir = Path.Combine(Program.GamesArchivesDir, modName);
                if (!Directory.Exists(modDir))
                {
                    Directory.CreateDirectory(modDir);
                }
                FolderBrowserDialog fd = new()
                {
                    Description= "Select directory to extract the archive",
                    ShowNewFolderButton = true,
                    UseDescriptionForTitle = true
                };
                fd.ShowDialog();
                string extractDir = fd.SelectedPath;
                if (string.IsNullOrEmpty(extractDir))
                {
                    throw new DirectoryNotFoundException(extractDir);
                }
                Task.Run(async () => 
                { 
                    await Program.HttpSender.DownloadModFileAsync(modName, modDir, extractDir);
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorizedInfo result = Program.HttpSender.CheckAuthorized();
            string roles = result.Roles != null ? string.Join(Environment.NewLine, result.Roles) : "ERROR";
            MessageBox.Show($"UserName: {result.Name}{Environment.NewLine}Roles: {roles}", "Authorization info");
        }
    }
}
