namespace D2Launcher.Resources.Widgets
{
    public partial class InfoWidget : BaseD2Widget
    {
        public InfoWidget() : base()
        {
            InitializeComponent();
            OnShowingWidget += UpdateInfoTextBox;
        }

        private void UpdateInfoTextBox()
        {
            string text = Program.HttpSender.GetInfo();
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            InfoTextBox.Text = text;
        }
    }
}
