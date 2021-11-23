using System;
using System.Collections.Generic;

namespace day03
{
    internal struct Point2D : IEquatable<Point2D>
    {
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

        internal IEnumerable<Point2D> Move(string move)
        {
            int dx = 0;
            int dy = 0;
            char direction = move[0];
            int magnitude = int.Parse(move.Substring(1));

            switch (direction)
            {
                case 'U':
                    dy = 1;
                    break;
                case 'D':
                    dy = -1;
                    break;
                case 'R':
                    dx = 1;
                    break;
                case 'L':
                    dx = -1;
                    break;
                default:
                    throw new ArgumentException();
            }

            for (int i = 1; i < magnitude + 1; i++)
            {
                yield return new Point2D(){ X = this.X + dx * i, Y = this.Y + dy * i};
            }
        }
    }
}