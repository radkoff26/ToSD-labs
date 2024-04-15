using Lab3.data;
using Lab3.repository;
using Lab3.scenario;
using Lab3.scenario.impl;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private readonly Store store;
        private ScreenType currentScreenType;
        private Scenario scenario;
        private RefreshCallback refreshCallback;
        private SettingsRepository settingsRepository;

        public Form1()
        {
            store = new Store();
            currentScreenType = store.State.ScreenType;
            settingsRepository = new SettingsRepository();
            refreshCallback = new RefreshCallbackImpl(this);
            scenario = GetScenarioByScreenType(currentScreenType);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            store.StateUpdate += OnNewState;
            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            MouseMove += OnMouseMove;
            KeyDown += OnKeyPressedEvent;
            KeyUp += OnKeyUnpressedEvent;
            KeyPreview = true;
        }

        private void OnNewState(State state)
        {
            ScreenType screenType = state.ScreenType;
            if (screenType != currentScreenType)
            {
                scenario.OnDestroy();
                scenario = GetScenarioByScreenType(screenType);
                scenario.OnCreate();
                currentScreenType = screenType;
            }
        }

        private Scenario GetScenarioByScreenType(ScreenType screenType)
        {
            switch (screenType)
            {
                case ScreenType.Points:
                    return new PointsScenario(store, refreshCallback, settingsRepository);
                case ScreenType.CurvedLines:
                    return new CurvedLinesScenario(store, refreshCallback, settingsRepository);
                case ScreenType.BrokenLines:
                    return new BrokenLinesScenario(store, refreshCallback, settingsRepository);
                case ScreenType.BezierLines:
                    return new BezierLinesScenario(store, refreshCallback, settingsRepository);
                case ScreenType.FilledLines:
                    return new FilledLinesScenario(store, refreshCallback, settingsRepository);
                case ScreenType.Movement:
                    return new MovementScenario(store, refreshCallback, settingsRepository, this);
            }
            throw new NotImplementedException();
        }

        private void ClearForm()
        {
            store.updateState(state =>
            {
                return new State(ScreenType.Points, []);
            });
            scenario.OnDestroy();
            scenario = new PointsScenario(store, refreshCallback, settingsRepository);
            scenario.OnCreate();
            currentScreenType = ScreenType.Points;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            scenario.OnDraw(e, this);
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            scenario.OnMouseUp(e);
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            scenario.OnMouseDown(e);
        }

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            scenario.OnMouseMove(e);
        }

        private void OnKeyPressedEvent(object? sender, KeyEventArgs e)
        {
            ProcessKeyEvent(e, true);
        }

        private void OnKeyUnpressedEvent(object? sender, KeyEventArgs e)
        {
            ProcessKeyEvent(e, false);
        }
        
        private void ProcessKeyEvent(KeyEventArgs e, bool pressed)
        {
            e.Handled = true;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    scenario.OnKeyUp(e, pressed);
                    break;
                case Keys.Right:
                    scenario.OnKeyRight(e, pressed);
                    break;
                case Keys.Down:
                    scenario.OnKeyDown(e, pressed);
                    break;
                case Keys.Left:
                    scenario.OnKeyLeft(e, pressed);
                    break;
                case Keys.Escape:
                    if (pressed)
                    {
                        ClearForm();
                    }
                    break;
                case Keys.Add:
                    scenario.OnKeyAdd(e, pressed);
                    break;
                case Keys.Subtract:
                    scenario.OnKeySubtract(e, pressed);
                    break;
                case Keys.Space:
                    scenario.OnKeySpace(e, pressed);
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }

        /* Click Handlers */
        private void OnPointsButtonClick(object sender, EventArgs e) {
            store.updateState(state =>
            {
                return new State(ScreenType.Points, state.Points);
            });
        }

        private void OnSettingsButtonClick(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }

        private void OnCurvedLinesButtonClick(object sender, EventArgs e)
        {
            store.updateState(state =>
            {
                return new State(ScreenType.CurvedLines, state.Points);
            });
        }

        private void OnBrokenLinesButtonClick(object sender, EventArgs e)
        {
            store.updateState(state =>
            {
                return new State(ScreenType.BrokenLines, state.Points);
            });
        }

        private void OnBezierLinesButtonClick(object sender, EventArgs e)
        {
            store.updateState(state =>
            {
                return new State(ScreenType.BezierLines, state.Points);
            });
        }

        private void OnFilledLinesButtonClick(object sender, EventArgs e)
        {
            store.updateState(state =>
            {
                return new State(ScreenType.FilledLines, state.Points);
            });
        }

        private void OnMovementButtonClick(object sender, EventArgs e)
        {
            store.updateState(state =>
            {
                return new State(ScreenType.Movement, state.Points);
            });
        }

        private void OnClearButtonClick(object sender, EventArgs e) {
            ClearForm();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                store.StateUpdate -= OnNewState;
                MouseDown -= OnMouseDown;
                MouseUp -= OnMouseUp;
                MouseMove -= OnMouseMove;
            }
            base.Dispose(disposing);
        }

        private class RefreshCallbackImpl(Form form) : RefreshCallback
        {
            public void Refresh()
            {
                form.Refresh();
            }
        }
    }
}
