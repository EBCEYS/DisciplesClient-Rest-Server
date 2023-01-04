namespace AdminAndAuthorClient.UserForms.UserCreationForms
{
    public partial class UserCreationForm : Form
    {
        public UserCreationForm()
        {
            InitializeComponent();
        }

        private void CreateButton1_Click(object sender, EventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Text;
            string email = EmailTextBox.Text;
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Plz enter username!");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Plz enter password!");
                return;
            }
            if (RolesChekListBox.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Plz select at least one role!");
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                email = null;
            }
            List<string> roles = new();
            foreach(object item in RolesChekListBox.CheckedItems)
            {
                roles.Add(item.ToString());
            }
            MessageBox.Show($"Creating result: {Program.HttpSender.CreateUser(userName, password, email ?? null, roles.ToArray())}");
        }

        private void UserCreationForm_Load(object sender, EventArgs e)
        {
            RolesChekListBox.CheckOnClick = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
