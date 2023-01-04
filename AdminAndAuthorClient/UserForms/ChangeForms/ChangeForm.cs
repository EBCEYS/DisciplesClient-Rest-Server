namespace AdminAndAuthorClient.UserForms.ChangeForms
{
    public partial class ChangeForm : Form
    {
        private readonly ChangeTypes type;

        public ChangeForm(ChangeTypes type)
        {
            InitializeComponent();
            this.type = type;
            this.Text = $"Change {type} form";
        }

        private void ChangeForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeModelFactory factory = new(type, CurrentTextBox.Text, NewTextBox.Text);
                if (Program.HttpSender.ChangeRequest(type, factory.Create()))
                {
                    MessageBox.Show("Changed successfuly!");
                    Program.HttpSender.CheckAuthorized();
                }
                else
                {
                    MessageBox.Show($"{type} has not changed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
    }
}
