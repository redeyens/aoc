using System;
using System.Collections.Generic;

namespace day03
{
    internal class Wire
    {
        private HashSet<Point2D> wirePoints;

        private Wire(HashSet<Point2D> wirePoints)
        {
            this.wirePoints = wirePoints;
        }

        internal static Wire FromMoves(string moves)
        {
            HashSet<Point2D> newWirePoints = new HashSet<Point2D>();
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

        internal IEnumerable<Point2D> Intersect(Wire wire)
        {
            var result = new HashSet<Point2D>(wirePoints);

            result.IntersectWith(wire.wirePoints);

            return result;
        }
    }
}