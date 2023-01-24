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

            HideAllWidgets();
            //TODO: сейчас при работе с модами не обновляются списки в виджетах (обновляются только после перезапуска)
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
    }
}
