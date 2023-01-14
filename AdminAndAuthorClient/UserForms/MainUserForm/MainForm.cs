using AdminAndAuthorClient.UserDataStorage;
using AdminAndAuthorClient.UserForms.ByIdForms;
using AdminAndAuthorClient.UserForms.ChangeForms;
using AdminAndAuthorClient.UserForms.ModsForms;
using AdminAndAuthorClient.UserForms.UserCreationForms;
using Disciples2ApiModels.ApiModels;
using Disciples2ApiModels.D2ApiModels;

namespace AdminAndAuthorClient.UserForms.MainUserForm
{
    public partial class MainForm : Form
    {
        private ModInfo[] mods;
        private ModInfo[] soft;
        private const string labelText = "Disciples2 admin client";
        public MainForm()
        {
            InitializeComponent();
            Program.HttpSender.UnAuthorizedError += LoginForm;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!UserStorage.IsParamsSet())
            {
                LoginForm();
            }
            UpdateModsDataGrid();
        }

        private void LoginForm()
        {
            LoginForm loginForm = new();
            loginForm.ShowDialog();
            if (!UserStorage.IsParamsSet())
            {
                this.Close();
            }
        }

        private void UpdateModsDataGrid()
        {
            mods = Program.HttpSender.GetModsInfo() ?? mods;
            if (mods is not null)
            {
                ModsListDataGrid.DataSource = mods;
                ModsListDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                CreateModsDirs(mods);
            }
            soft = Program.HttpSender.GetSoftInfo() ?? soft;
            if (soft is not null)
            {
                SoftListDataGridView.DataSource = soft;
                SoftListDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                CreateModsDirs(soft);
            }
        }

        private static void CreateModsDirs(ModInfo[] array)
        {
            foreach(ModInfo mod in array)
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
            int selectedSoftRowsCount = SoftListDataGridView.SelectedRows.Count;
            if (selectedRowsCount <= 0 && selectedSoftRowsCount <= 0)
            {
                throw new Exception("Select row in one of tables");
            }
            if (selectedSoftRowsCount > 0)
            {
                DataGridViewRow softRow = SoftListDataGridView.SelectedRows[0];
                string softName = softRow.Cells[0].Value.ToString();
                return softName;
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
                this.Text = "Downloading...";
                Task.Run(async () => await Program.HttpSender.DownloadModFileAsync(modName, modDir, extractDir)).Wait();
                this.Text = labelText;
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
            MessageBox.Show($"Id: {result.Id}{Environment.NewLine}UserName: {result.Name}{Environment.NewLine}Roles: {roles}", "Authorization info");
        }

        private void GetUserByIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessUserByIdForm form = new(OperationByUserId.Get);
            form.ShowDialog();
        }

        private void DeleteUserByIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessUserByIdForm form = new(OperationByUserId.Delete);
            form.ShowDialog();
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserCreationForm form = new();
            form.ShowDialog();
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = $"If you have a questions about system you can go to github{Environment.NewLine}{Program.GitHub}{Environment.NewLine}or msg me to discord{Environment.NewLine}{Program.Discord}!";
            MessageBox.Show(info, "Info");
        }

        private void UserNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForm form = new(ChangeTypes.username);
            form.ShowDialog();
        }

        private void PasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForm form = new(ChangeTypes.password);
            form.ShowDialog();
        }

        private void EmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeForm form = new(ChangeTypes.email);
            MessageBox.Show("If you haven't entered the email on registration, stay current field empty,", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            form.ShowDialog();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.HttpSender.Logout();
            LoginForm loginForm = new();
            loginForm.ShowDialog();
        }

        private void ModsListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SoftListDataGridView.ClearSelection();
        }

        private void SoftListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ModsListDataGrid.ClearSelection();
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = GetSelectedModName();
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            ModInfo? info = null;
            bool isSoftware = false;
            if (mods.Any(m => m.Name == name))
            {
                info = mods.First(m => m.Name == name);
            }
            if (soft.Any(s => s.Name == name))
            {
                info = soft.First(s => s.Name == name);
                isSoftware = true;
            }
            if (info is null)
            {
                MessageBox.Show("Plz select one of row from tables!");
                return;
            }
            UploadModForm form = new(info, isSoftware);
            form.ShowDialog();
            UpdateModsDataGrid();
        }
    }
}
