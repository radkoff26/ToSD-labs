using static Lab3.FigureMovementController;

namespace Lab3
{
    internal class FigureMovementController(List<Point> initialPoints, DrawFigure drawFigureCallback)
    {
        private readonly Pen trailPen = new Pen(Color.FromArgb(100, 0, 0, 0));
        // Int array of size 2, where the first item is x, the other - y.
        private readonly List<int[]> deltas = new List<int[]>();

        private readonly List<Point> initialPoints = new(initialPoints);
        public List<Point> CurrentPoints { get; private set; } = new(initialPoints);

        public delegate void DrawFigure(PaintEventArgs e, List<Point> points, Pen pen);
        private DrawFigure drawFigureCallback = drawFigureCallback;

        public delegate void OnNewPoints(List<Point> points);
        public event OnNewPoints? NewPointsEvent;

        public void MoveUp()
        {
            deltas.Add([0, -1]);
            CurrentPoints = ShiftPoints(0, -1, CurrentPoints);
            NewPointsEvent?.Invoke(CurrentPoints);
        }

        public void MoveDown()
        {
            deltas.Add([0, 1]);
            CurrentPoints = ShiftPoints(0, 1, CurrentPoints);
            NewPointsEvent?.Invoke(CurrentPoints);
        }

        public void MoveLeft()
        {
            deltas.Add([-1, 0]);
            CurrentPoints = ShiftPoints(-1, 0, CurrentPoints);
            NewPointsEvent?.Invoke(CurrentPoints);
        }

        public void MoveRight()
        {
            deltas.Add([1, 0]);
            CurrentPoints = ShiftPoints(1, 0, CurrentPoints);
            NewPointsEvent?.Invoke(CurrentPoints);
        }

        private List<Point> ShiftPoints(int x, int y, List<Point> currentPoints)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < currentPoints.Count; i++)
            {
                Point current = currentPoints[i];
                points.Add(new Point(current.X + x, current.Y + y));
            }
            return points;
        }

        public void OnDraw(PaintEventArgs e)
        {
            List<Point> points = initialPoints;
            deltas.ForEach(delta =>
            {
                drawFigureCallback.Invoke(e, points, trailPen);
                points = ShiftPoints(delta[0], delta[1], points);
            });
        }
    }
}
