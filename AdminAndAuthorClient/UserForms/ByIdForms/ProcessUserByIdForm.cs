using Disciples2ClientDataBaseModels.DBModels;

namespace AdminAndAuthorClient.UserForms.ByIdForms
{
    public partial class ProcessUserByIdForm : Form
    {
        private readonly OperationByUserId operation;

        public ProcessUserByIdForm(OperationByUserId operation)
        {
            InitializeComponent();
            this.Text = $"{operation} user by id";
            this.operation = operation;
        }

        private void ProcessUserByIdForm_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("Can not read user id!");
                return;
            }
            if (operation == OperationByUserId.Delete)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string result = Program.HttpSender.DeleteUserById(id) ? "OK" : "user not found";
                    MessageBox.Show($"Operation result is {result}!");
                }
            }
            else
            {
                User usr = Program.HttpSender.GetUserById(id);
                if (usr != null)
                {
                    MessageBox.Show($"ID: {usr.Id}{Environment.NewLine}USERNAME: {usr.UserName}{Environment.NewLine}EMAIL: {usr.Email}{Environment.NewLine}ROLES: {string.Join(", ", usr.Roles)}{Environment.NewLine}IS ACTIVE: {usr.IsActive}");
                }
            }
            this.Close();
        }
    }
}
