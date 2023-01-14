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
            if (OnShowingWidget != null)
            {
                OnShowingWidget!.Invoke();
            }
            this.Enabled = true;
            this.Visible = true;
            this.Show();
        }

        public void HideWidget()
        {
            if (OnHiddenWidget != null)
            {
                OnHiddenWidget!.Invoke();
            }
            this.Enabled = false;
            this.Visible = false;
            this.Hide();
        }
    }
}
