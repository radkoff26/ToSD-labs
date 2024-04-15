using Lab3.data;
using Lab3.utils;

namespace Lab3
{
    partial class SettingsForm
    {
        private const int FORM_WIDTH = 400;
        private const int FORM_HEIGHT = 300;
        private const int BOX_WIDTH = 200;
        private const int BOX_HEIGHT = 30;
        private const int HORIZONTAL_PADDING = 40;
        private const int BUTTON_WIDTH = 100;
        private const int BUTTON_HEIGHT = 30;

        private Label lineColorText;
        private Label pointColorText;

        private Button lineColorChangeButton;
        private Button pointsColorChangeButton;
        
        private void InitializeComponent()
        {
            InitializeForm();
            InitializeElements();
        }

        private void InitializeForm()
        {
            ClientSize = new Size(FORM_WIDTH, FORM_HEIGHT);
            Name = "Параметры";
            Text = "Параметры";
        }

        private void InitializeElements()
        {
            Settings settings = repository.GetCurrentSettings();
            InitializeLineColorTextBox(settings.lineColor);
            InitializePointsColorTextBox(settings.pointColor);
            InitializeLineColorChangeButton();
            InitializePointsColorChangeButton();
        }

        private void InitializeLineColorChangeButton()
        {
            lineColorChangeButton = new Button
            {
                Text = "Выбрать",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(HORIZONTAL_PADDING + BOX_WIDTH + 20, 80)
            };
            lineColorChangeButton.Click += OnLineColorChangeButtonClick;
            Controls.Add(lineColorChangeButton);
        }

        private void InitializePointsColorChangeButton()
        {
            pointsColorChangeButton = new Button
            {
                Text = "Выбрать",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(HORIZONTAL_PADDING + BOX_WIDTH + 20, 190)
            };
            pointsColorChangeButton.Click += OnPointsColorChangeButtonClick;
            Controls.Add(pointsColorChangeButton);
        }

        private void InitializeLineColorTextBox(Color lineColor)
        {
            lineColorText = new Label
            {
                Text = "Цвет линий: " + ColorUtils.ColorToHexString(lineColor),
                ForeColor = lineColor,
                Height = BOX_HEIGHT,
                Width = BOX_WIDTH,
                Location = new Point(HORIZONTAL_PADDING, 80)
            };
            Controls.Add(lineColorText);
        }

        private void UpdateLineColorTextBox(Color lineColor)
        {
            lineColorText.Text = "Цвет линий: " + ColorUtils.ColorToHexString(lineColor);
            lineColorText.ForeColor = lineColor;
        }

        private void InitializePointsColorTextBox(Color pointsColor)
        {
            pointColorText = new Label
            {
                Text = "Цвет точек: " + ColorUtils.ColorToHexString(pointsColor),
                ForeColor = pointsColor,
                Height = BOX_HEIGHT,
                Width = BOX_WIDTH,
                Location = new Point(HORIZONTAL_PADDING, 190)
            };
            Controls.Add(pointColorText);
        }

        private void UpdatePointsColorTextBox(Color pointsColor)
        {
            pointColorText.Text = "Цвет точек: " + ColorUtils.ColorToHexString(pointsColor);
            pointColorText.ForeColor = pointsColor;
        }
    }
}