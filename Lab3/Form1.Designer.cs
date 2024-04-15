namespace Lab3
{
    partial class Form1
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 600;
        private const int BUTTONS_LEFT_MARGIN = 16;
        private const int BUTTON_HEIGHT = 50;
        private const int BUTTON_WIDTH = 120;
        public static Rectangle CANVAS_BOUNDS = new Rectangle(
            x: BUTTONS_LEFT_MARGIN * 2 + BUTTON_WIDTH,
            y: 0,
            width: WIDTH - BUTTONS_LEFT_MARGIN * 2 - BUTTON_WIDTH,
            height: HEIGHT
        );

        private System.Windows.Forms.Button pointsButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button curvedLinesButton;
        private System.Windows.Forms.Button brokenLinesButton;
        private System.Windows.Forms.Button bezierLinesButton;
        private System.Windows.Forms.Button filledLinesButton;
        private System.Windows.Forms.Button movementButton;
        private System.Windows.Forms.Button clearFormButton;

        private void InitializeComponent()
        {
            InitializeForm();
            InitializeButtons();
        }

        /* Initializers */
        private void InitializeForm()
        {
            ClientSize = new Size(WIDTH, HEIGHT);
            Name = "Форма";
            Text = "Форма";
            Load += Form1_Load;
        }

        private void InitializeButtons()
        {
            InitializePointsButton();
            InitializeSettingsButton();
            InitializeCurvedLinesButton();
            InitializeBrokenLinesButton();
            InitializeBezierLinesButton();
            InitializeFilledLinesButton();
            InitializeMovementButton();
            InitializeClearButton();
        }

        private void InitializePointsButton()
        {
            pointsButton = new System.Windows.Forms.Button()
            {
                Text = "Точки",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 22)
            };
            pointsButton.Click += this.OnPointsButtonClick;
            Controls.Add(pointsButton);
        }

        private void InitializeSettingsButton()
        {
            settingsButton = new System.Windows.Forms.Button()
            {
                Text = "Параметры",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 94)
            };
            settingsButton.Click += this.OnSettingsButtonClick;
            Controls.Add(settingsButton);
        }

        private void InitializeCurvedLinesButton()
        {
            curvedLinesButton = new System.Windows.Forms.Button()
            {
                Text = "Кривая",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 167)
            };
            curvedLinesButton.Click += this.OnCurvedLinesButtonClick;
            Controls.Add(curvedLinesButton);
        }

        private void InitializeBrokenLinesButton()
        {
            brokenLinesButton = new System.Windows.Forms.Button()
            {
                Text = "Ломаная",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 239)
            };
            brokenLinesButton.Click += this.OnBrokenLinesButtonClick;
            Controls.Add(brokenLinesButton);
        }

        private void InitializeBezierLinesButton()
        {
            bezierLinesButton = new System.Windows.Forms.Button()
            {
                Text = "Безье",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 311)
            };
            bezierLinesButton.Click += this.OnBezierLinesButtonClick;
            Controls.Add(bezierLinesButton);
        }

        private void InitializeFilledLinesButton()
        {
            filledLinesButton = new System.Windows.Forms.Button()
            {
                Text = "Заполненная",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 383)
            };
            filledLinesButton.Click += this.OnFilledLinesButtonClick;
            Controls.Add(filledLinesButton);
        }

        private void InitializeMovementButton()
        {
            movementButton = new System.Windows.Forms.Button()
            {
                Text = "Движение",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 456)
            };
            movementButton.Click += this.OnMovementButtonClick;
            Controls.Add(movementButton);
        }

        private void InitializeClearButton()
        {
            clearFormButton = new System.Windows.Forms.Button()
            {
                Text = "Очистить",
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                Location = new Point(BUTTONS_LEFT_MARGIN, 528)
            };
            clearFormButton.Click += this.OnClearButtonClick;
            Controls.Add(clearFormButton);
        }
    }
}
