using System;
using System.Collections.Generic;

namespace day03
{
    internal class Wire
    {
        private List<Point2D> wirePoints;

        private Wire(List<Point2D> wirePoints)
        {
            this.wirePoints = wirePoints;
        }

        internal static Wire FromMoves(string moves)
        {
            List<Point2D> newWirePoints = new List<Point2D>();
            string[] moveArray = moves.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Point2D currentLocation = new Point2D();

            foreach (string move in moveArray)
            {
                foreach (Point2D point in currentLocation.Move(move))
                {
                    newWirePoints.Add(point);
                    currentLocation = point;
                }
            }

            return new Wire(newWirePoints);
        }

        internal int GetTraceDistance(Point2D p)
        {
            return wirePoints.IndexOf(p) + 1;
        }

        internal IEnumerable<Point2D> Intersect(Wire wire)
        {
            var result = new HashSet<Point2D>(wirePoints);

            result.IntersectWith(wire.wirePoints);

            return result;
        }
    }
}