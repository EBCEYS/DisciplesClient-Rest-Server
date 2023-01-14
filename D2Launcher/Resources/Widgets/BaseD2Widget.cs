namespace D2Launcher.Resources.Widgets
{
    public partial class BaseD2Widget : UserControl
    {
        public event Action OnShowingWidget;
        public event Action OnHiddenWidget;
        public BaseD2Widget()
        {
            InitializeComponent();
        }

        public void ShowWidget()
        {
            this.Enabled = true;
            this.Visible = true;
            if (OnShowingWidget != null)
            {
                OnShowingWidget!.Invoke();
            }
            this.Show();
        }

        public void HideWidget()
        {
            this.Enabled = false;
            this.Visible = false;
            if (OnHiddenWidget != null)
            {
                OnHiddenWidget!.Invoke();
            }
            this.Hide();
        }
    }
}
