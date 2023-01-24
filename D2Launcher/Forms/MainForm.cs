using D2Launcher.Resources.Widgets;

namespace D2Launcher.Forms
{
    public partial class MainForm : Form
    {
        List<BaseD2Widget> Widgets { get; } = new();
        public MainForm()
        {
            InitializeComponent();

            Widgets.Add(ModsListWidget);
            Widgets.Add(InstalledModsWidget);
            Widgets.Add(SoftWidget);
            Widgets.Add(InfoWidget);

            HideAllWidgets();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void D2MainMenuButton1_Click(object sender, EventArgs e)
        {
            HideAllWidgets();
            ModsListWidget.ShowWidget();
        }

        private void HideAllWidgets()
        {
            Widgets.ForEach(x => x.HideWidget());
        }

        private void InstalledModsButton_Click(object sender, EventArgs e)
        {
            HideAllWidgets();
            InstalledModsWidget.ShowWidget();
        }

        private void SoftwareButton_Click(object sender, EventArgs e)
        {
            HideAllWidgets();
            SoftWidget.ShowWidget();
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            HideAllWidgets();
            InfoWidget.ShowWidget();
        }
    }
}
