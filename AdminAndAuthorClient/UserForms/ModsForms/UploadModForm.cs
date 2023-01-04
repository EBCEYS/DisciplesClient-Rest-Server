namespace AdminAndAuthorClient.UserForms.ModsForms
{
    public partial class UploadModForm : Form
    {
        public UploadModForm()
        {
            InitializeComponent();
        }

        private string FileDir { get; set; }
        private string ModName => ModNameTextBox.Text;
        private string ModVersion => ModVersionTextBox.Text;
        private void UploadModForm_Load(object sender, EventArgs e)
        {

        }

        private void SelectArchiveButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new()
            {
                Filter = "Mod archive (*.zip)|*.zip",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };
            fd.ShowDialog();
            FileDir = fd.FileName;
            textBox1.Text = FileDir;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileDir))
            {
                MessageBox.Show("Select the mod archive file");
                return;
            }
            if (!File.Exists(FileDir))
            {
                MessageBox.Show($"File not found! {FileDir}");
                return;
            }
            if (string.IsNullOrEmpty(ModName))
            {
                MessageBox.Show("Plz enter mod name!");
                return;
            }
            if (string.IsNullOrEmpty(ModVersion))
            {
                MessageBox.Show("Plz enter mod version!");
                return;
            }
            Program.HttpSender.UploadModFileAync(ModName, ModVersion, FileDir, null);
        }
    }
}
