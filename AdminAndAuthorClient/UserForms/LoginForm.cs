using AdminAndAuthorClient.UserDataStorage;

namespace AdminAndAuthorClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            TryToLogin();
        }

        private void TryToLogin()
        {
            string userName = LoginTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Plz check your username and password!");
                return;
            }
            string token = Program.HttpSender.PostLoginRequest(userName, password);
            if (token is not null)
            {
                LoginTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;
                UserStorage.SetUserInfo(userName, password, token);
                MessageBox.Show($"Logined successfuly!");
                Program.HttpSender.SetToken(token);
                this.Close();
            }
        }
    }
}