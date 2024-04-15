﻿using Lab3.data;
using Lab3.repository;
using Lab3.utils;

namespace Lab3.scenario.impl
{
    internal class CurvedLinesScenario : Scenario
    {
        private FigureMovementController controller;
        private List<Point> currentPoints;
        private readonly Brush pointBrush;
        private readonly Pen linePen;
        private bool wasNotified;

        public CurvedLinesScenario(Store store, RefreshCallback callback, SettingsRepository repository) : base(store, callback)
        {
            currentPoints = store.State.Points;
            controller = new FigureMovementController(currentPoints, DrawTrailFigure);
            controller.NewPointsEvent += OnCurrentPointsChanged;
            Settings settings = repository.GetCurrentSettings();
            pointBrush = new SolidBrush(settings.pointColor);
            linePen = new Pen(settings.lineColor);
        }

        public override void OnDraw(PaintEventArgs e, Form form)
        {
            if (currentPoints.Count <= 2)
            {
                if (!wasNotified)
                {
                    AlertDisplayingUtils.ShowAlertDueToLackOfDataForDrawing();
                    wasNotified = true;
                }
                return;
            }
            wasNotified = false;
            Graphics graphics = e.Graphics;
            controller.OnDraw(e);
            graphics.DrawClosedCurve(linePen, currentPoints.ToArray());
            PointsDrawingUtils.DrawAllPoints(currentPoints, graphics, pointBrush);
        }

        public override void OnKeyUp(KeyEventArgs e, bool pressed)
        {
            controller.MoveUp();
        }

        public override void OnKeyDown(KeyEventArgs e, bool pressed)
        {
            controller.MoveDown();
        }

        public override void OnKeyRight(KeyEventArgs e, bool pressed)
        {
            controller.MoveRight();
        }

        public override void OnKeyLeft(KeyEventArgs e, bool pressed)
        {
            controller.MoveLeft();
        }

        private void OnCurrentPointsChanged(List<Point> points)
        {
            currentPoints = points;
            callback.Refresh();
        }

        private void DrawTrailFigure(PaintEventArgs e, List<Point> points, Pen pen)
        {
            e.Graphics.DrawClosedCurve(pen, points.ToArray());
        }

        public override void OnDestroy()
        {
            controller.NewPointsEvent -= OnCurrentPointsChanged;
        }

        protected override void OnNewState(State state)
        {
            currentPoints = state.Points;
            callback.Refresh();
        }
    }
}