using AdminAndAuthorClient.Http;

namespace AdminAndAuthorClient.UserForms.ModsForms
{
    public partial class FilesForm : Form
    {
        private string ModName { get; set; }
        public FilesForm(string modName)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(modName))
            {
                throw new ArgumentNullException(nameof(modName));
            }
            ModName = modName;
            this.Text = $"{ModName} files";
        }

        private string[] ModFiles { get; set; }
        private List<ModFileInfo> ModFilesInfo { get; set; }

        private void FilesForm_Load(object sender, EventArgs e)
        {
            FilesListDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            ModFiles = Program.HttpSender.GetModFiles(ModName);
            if (ModFiles is null)
            {
                MessageBox.Show("Empty mod files list!");
                this.Close();
                return;
            }
            ModFilesInfo = ModFileInfo.CreateArray(ModFiles);
            FilesListDataGridView.DataSource = ModFilesInfo;
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogResult != DialogResult.Yes)
            {
                return;
            }
            try
            {
                string fileName = GetFileNameFromDataGrid();
                Program.HttpSender.DeleteModFile(ModName, fileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetFileNameFromDataGrid()
        {
            int selectedRowsCount = FilesListDataGridView.SelectedRows.Count;
            if (selectedRowsCount <= 0)
            {
                throw new Exception("Select file in table");
            }
            DataGridViewRow mod = FilesListDataGridView.SelectedRows[0];
            return mod.Cells[0].Value.ToString();
        }
    }

    class ModFileInfo
    {
        public string FileName { get; set; }

        public ModFileInfo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"\"{nameof(fileName)}\" не может быть неопределенным или пустым.", nameof(fileName));
            }
            FileName = fileName;
        }
        public static List<ModFileInfo> CreateArray(string[] files)
        {
            List<ModFileInfo> result = new();
            foreach(string file in files)
            {
                result.Add(new(file));
            }
            return result;
        }
    }
}
