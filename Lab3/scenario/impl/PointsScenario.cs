using Lab3.data;
using Lab3.repository;
using Lab3.utils;
using System.Drawing;

namespace Lab3.scenario.impl
{
    internal class PointsScenario : Scenario
    {
        private List<Point> points = new List<Point>();
        private Brush pointBrush;
        private int draggingPointIndex = -1;
        private float draggingDeltaX = 0;
        private float draggingDeltaY = 0;

        public PointsScenario(Store store, RefreshCallback callback, SettingsRepository repository): base(store, callback)
        {
            Settings settings = repository.GetCurrentSettings();
            pointBrush = new SolidBrush(settings.pointColor);
        }

        public override void OnCreate()
        {
            points = new List<Point>(store.State.Points);
            callback.Refresh();
        }

        public override void OnDraw(PaintEventArgs e, Form form)
        {
            Graphics g = e.Graphics;
            PointsDrawingUtils.DrawAllPoints(points, g, pointBrush);
        }

        protected override void OnNewState(State state)
        {
            points = new List<Point>(state.Points);
            callback.Refresh();
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            Point point = e.Location;
            if (!IsClickedWithinUsedArea(point))
            {
                return;
            }
            int index = GetHoveredPoint(point);
            if (index != -1)
            {
                Point clickedPoint = points[index];
                draggingPointIndex = index;
                draggingDeltaX = point.X - clickedPoint.X;
                draggingDeltaY = point.Y - clickedPoint.Y;
            }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (draggingPointIndex != -1 && draggingPointIndex < points.Count)
            {
                Point point = e.Location;
                Point draggingPoint = points[draggingPointIndex];
                draggingPoint.X = Math.Min(Math.Max((int)(point.X - draggingDeltaX), Form1.CANVAS_BOUNDS.Left), Form1.CANVAS_BOUNDS.Right);
                draggingPoint.Y = Math.Min(Math.Max((int)(point.Y - draggingDeltaY), Form1.CANVAS_BOUNDS.Top), Form1.CANVAS_BOUNDS.Bottom);
                points[draggingPointIndex] = draggingPoint;
                store.updateState(state => new State(state.ScreenType, points));
                callback.Refresh();
            }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            if (draggingPointIndex != -1)
            {
                StopDragging();
            } else
            {
                Point point = e.Location;
                if (!IsClickedWithinUsedArea(point))
                {
                    return;
                }
                points.Add(point);
                store.updateState(state => new State(state.ScreenType, points));
                callback.Refresh();
            }
        }

        private bool IsClickedWithinUsedArea(Point point)
        {
            return point.X >= Form1.CANVAS_BOUNDS.Left && point.X <= Form1.CANVAS_BOUNDS.Right && point.Y >= Form1.CANVAS_BOUNDS.Top && point.Y <= Form1.CANVAS_BOUNDS.Bottom;
        }

        private void StopDragging()
        {
            draggingPointIndex = -1;
            draggingDeltaX = 0;
            draggingDeltaY = 0;
        }

        private int GetHoveredPoint(Point point)
        {
            float radius = PointsDrawingUtils.POINT_RADIUS;
            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];
                float startX = p.X - radius;
                float endX = p.X + radius;
                float startY = p.Y - radius;
                float endY = p.Y + radius;
                if (point.X >= startX && point.X <= endX && point.Y >= startY && point.Y <= endY)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
