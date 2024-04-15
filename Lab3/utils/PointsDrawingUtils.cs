namespace Lab3.utils
{
    internal class PointsDrawingUtils
    {
        public const float POINT_RADIUS = 7f;

        public static void DrawAllPoints(List<Point> points, Graphics g, Brush brush)
        {
            points.ForEach(p =>
            {
                DrawPoint(p, g, brush);
            });
        }

        public static void DrawPoint(Point point, Graphics g, Brush brush)
        {
            g.FillEllipse(brush, point.X - POINT_RADIUS, point.Y - POINT_RADIUS, POINT_RADIUS * 2, POINT_RADIUS * 2);
        }
    }
}
