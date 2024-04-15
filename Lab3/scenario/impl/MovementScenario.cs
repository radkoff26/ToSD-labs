using Lab3.data;
using Lab3.repository;
using Lab3.utils;

namespace Lab3.scenario.impl
{
    internal class MovementScenario : Scenario
    {
        private const int MAX_POINTS_SPEED = 7;
        private const int MIN_POINTS_SPEED = 1;
        private List<Point> movingPoints = [
            new Point(Form1.CANVAS_BOUNDS.Left, Form1.CANVAS_BOUNDS.Top + 50),    
            new Point(Form1.CANVAS_BOUNDS.Left, Form1.CANVAS_BOUNDS.Top + 150),    
            new Point(Form1.CANVAS_BOUNDS.Left, Form1.CANVAS_BOUNDS.Top + 250)    
        ];
        private Form form;
        private List<Point> figurePoints = new List<Point>();
        private Rectangle figureBounds = new Rectangle();
        private Brush pointBrush;
        private Brush figureBrush;
        private System.Windows.Forms.Timer moveTimer = new System.Windows.Forms.Timer();
        private int currentPointsSpeed = MIN_POINTS_SPEED;
        private int directionFactor = 1;
        private bool wasNotified;

        public MovementScenario(Store store, RefreshCallback callback, SettingsRepository repository, Form form): base(store, callback)
        {
            Settings settings = repository.GetCurrentSettings();
            pointBrush = new SolidBrush(settings.pointColor);
            figureBrush = new SolidBrush(settings.lineColor);
            moveTimer.Interval = 16;
            moveTimer.Tick += OnTimerTick;
            this.form = form;
        }

        public override void OnCreate()
        {
            figurePoints = new List<Point>(store.State.Points);
            UpdateFigureBounds();
            callback.Refresh();
        }

        public override void OnDraw(PaintEventArgs e, Form form)
        {
            if (figurePoints.Count <= 1)
            {
                if (!wasNotified)
                {
                    AlertDisplayingUtils.ShowAlertDueToLackOfDataForDrawing();
                    wasNotified = true;
                }
                return;
            }
            Graphics g = e.Graphics;
            g.FillClosedCurve(figureBrush, figurePoints.ToArray());
            PointsDrawingUtils.DrawAllPoints(movingPoints, g, pointBrush);
        }

        public void OnTimerTick(object? sender, EventArgs e)
        {
            MovePoints();
            MirrorFigureIfNecessary();
            callback.Refresh();
        }

        private void UpdateFigureBounds()
        {
            int top = int.MaxValue;
            int bottom = 0;
            int left = int.MaxValue;
            int right = 0;
            figurePoints.ForEach(p => {
                if (p.X > right)
                {
                    right = p.X;
                }
                if (p.X < left)
                {
                    left = p.X;
                }
                if (p.Y > bottom)
                {
                    bottom = p.Y;
                }
                if (p.Y < top)
                {
                    top = p.Y;
                }
            });
            figureBounds = new Rectangle(left, top, right - left, bottom - top);
        }

        private void MovePoints()
        {
            for (int i = 0; i < movingPoints.Count; i++)
            {
                Point current = movingPoints[i];
                if (current.X + currentPointsSpeed * directionFactor > Form1.CANVAS_BOUNDS.Right)
                {
                    directionFactor = -1;
                } else if (current.X + currentPointsSpeed * directionFactor < Form1.CANVAS_BOUNDS.Left)
                {
                    directionFactor = 1;
                }
                movingPoints[i] = new Point(current.X + currentPointsSpeed * directionFactor, current.Y);
            }
        }

        private void MirrorFigureIfNecessary()
        {
            int viewportWidth = form.Width - Form1.CANVAS_BOUNDS.Left;
            int viewportHeight = form.Height - Form1.CANVAS_BOUNDS.Top;
            if (figureBounds.Right > form.Width && figureBounds.Width < viewportWidth)
            {
                MirrorHorizontally();
                UpdateFigureBounds();
            }
            if (figureBounds.Bottom > form.Height && figureBounds.Height < viewportHeight)
            {
                MirrorVertically();
                UpdateFigureBounds();
            }
        }

        private void MirrorHorizontally()
        {
            int centerX = (figureBounds.Left + figureBounds.Right) / 2;
            int left = figureBounds.Right - figureBounds.Width * 2;
            int mirrorLine = left + figureBounds.Width;
            int realLeft = Math.Max(left, Form1.CANVAS_BOUNDS.Left);
            int offset = realLeft - left;
            List<Point> points = [];
            figurePoints.ForEach(p =>
            {
                int distanceToMirrorLine = p.X - mirrorLine;
                points.Add(new Point(p.X - distanceToMirrorLine * 2 + offset, p.Y));
            });
            figurePoints = points;
        }

        private void MirrorVertically()
        {
            int centerY = (figureBounds.Top + figureBounds.Bottom) / 2;
            int top = figureBounds.Bottom - figureBounds.Height * 2;
            int mirrorLine = top + figureBounds.Height;
            int realTop = Math.Max(top, Form1.CANVAS_BOUNDS.Top);
            int offset = realTop - top;
            List<Point> points = [];
            figurePoints.ForEach(p =>
            {
                int distanceToMirrorLine = p.Y - mirrorLine;
                points.Add(new Point(p.X, p.Y - distanceToMirrorLine * 2 + offset));
            });
            figurePoints = points;
        }

        protected override void OnNewState(State state)
        {
            // Do nothing
        }

        public override void OnKeySpace(KeyEventArgs e, bool pressed)
        {
            if (pressed)
            {
                if (moveTimer.Enabled)
                {
                    moveTimer.Stop();
                }
                else
                {
                    moveTimer.Start();
                }
            }
        }

        public override void OnKeyAdd(KeyEventArgs e, bool pressed)
        {
            currentPointsSpeed = Math.Min(MAX_POINTS_SPEED, currentPointsSpeed + 1);
        }

        public override void OnKeySubtract(KeyEventArgs e, bool pressed)
        {
            currentPointsSpeed = Math.Max(MIN_POINTS_SPEED, currentPointsSpeed - 1);
        }

        public override void OnDestroy()
        {
            if (moveTimer.Enabled)
            {
                moveTimer.Stop();
            }
            moveTimer.Tick -= OnTimerTick;
        }
    }
}
