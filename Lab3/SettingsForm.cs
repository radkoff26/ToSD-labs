using Lab3.data;
using Lab3.repository;

namespace Lab3
{
    public partial class SettingsForm : Form
    {
        private SettingsRepository repository;

        public SettingsForm()
        {
            repository = new SettingsRepository();
            InitializeComponent();
        }

        public void OnLineColorChangeButtonClick(object sender, EventArgs e)
        {
            getNewColorViaDialog(color =>
            {
                Settings currentSettings = repository.GetCurrentSettings();
                repository.SetCurrentSettings(new Settings(currentSettings.pointColor, color));
                UpdateLineColorTextBox(color);
            });
        }

        public void OnPointsColorChangeButtonClick(object sender, EventArgs e)
        {
            getNewColorViaDialog(color =>
            {
                Settings currentSettings = repository.GetCurrentSettings();
                repository.SetCurrentSettings(new Settings(color, currentSettings.lineColor));
                UpdatePointsColorTextBox(color);
            });
        }

        private void getNewColorViaDialog(Action<Color> colorAction)
        {
            ColorDialog f = new ColorDialog();
            var res = f.ShowDialog();
            if (res == DialogResult.OK)
            {
                colorAction(f.Color);
            }
        }
    }
}
