namespace D2Launcher.Resources.Widgets
{
    public partial class BaseD2Widget : UserControl
    {
        public BaseD2Widget()
        {
            InitializeComponent();
        }

        public void ShowWidget()
        {
            this.Enabled = true;
            this.Visible = true;
            this.Show();
        }

        public void HideWidget()
        {
            this.Enabled = false;
            this.Visible = false;
            this.Hide();
        }
    }
}
