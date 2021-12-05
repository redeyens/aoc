using System;
using System.Linq;

namespace day05
{
    internal struct Point2D : IEquatable<Point2D>
    {
        public Point2D(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X {get;set;}
        public int Y {get;set;}

        public override bool Equals(object obj)
        {
            return obj is Point2D other && Equals(other);
        }

        public bool Equals(Point2D other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        internal static Point2D FromString(string v)
        {
            int[] coords = v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            return new Point2D(coords[0], coords[1]);
        }
    }
}