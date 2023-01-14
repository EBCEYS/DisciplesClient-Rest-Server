using Disciples2ApiModels.D2ApiModels;

namespace AdminAndAuthorClient.UserForms.ModsForms
{
    public partial class UploadModForm : Form
    {
        public UploadModForm(ModInfo? modInfo = null, bool isSoftware = false)
        {
            InitializeComponent();
            if (modInfo is not null)
            {
                ModNameTextBox.Text = modInfo.Value.Name;
                ModNameTextBox.Enabled = false;
                ModVersionTextBox.Text = modInfo.Value.Version;
                checkBox1.Checked = isSoftware;
                checkBox1.Enabled = false;
                this.Text = $"Update {modInfo.Value.Name} menu";
            }
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
            UploadStateLabel.Text = "Uploading...";

            if (!checkBox1.Checked)
            {
                Task.Run(async () => await Program.HttpSender.UploadModFileAync(ModName, ModVersion, FileDir, null)).Wait();
            }
            else
            {
                Task.Run(async () => await Program.HttpSender.UploadSoftFileAync(ModName, ModVersion, FileDir, null)).Wait();
            }
            UploadStateLabel.Text = "Done!";
        }
    }
}
