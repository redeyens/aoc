using System;
using System.Collections.Generic;

namespace day05
{
    internal class Line
    {
        private Point2D startPoint;
        private Point2D endPoint;

        public Line(Point2D startPoint, Point2D endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public bool IsVertical => startPoint.X == endPoint.X;
        public bool IsHorizontal => startPoint.Y == endPoint.Y;
        public IEnumerable<Point2D> Points 
        { 
            get
            {
                int dx = IsVertical ? 0 : (startPoint.X < endPoint.X) ? 1 : -1;
                int dy = IsHorizontal ? 0 : (startPoint.Y < endPoint.Y) ? 1 : -1;

                Point2D currentPoint = startPoint;

                for (int i = 0; i < Length; i++)
                {
                    yield return currentPoint;
                    currentPoint = new Point2D(currentPoint.X + dx, currentPoint.Y + dy);
                }

                yield return currentPoint;
            }
        }

        public int Length => Math.Max(Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));

        internal static Line FromString(string textLine)
        {
            string[] pointText = textLine.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);

            return new Line(Point2D.FromString(pointText[0]), Point2D.FromString(pointText[1]));
        }
    }
}