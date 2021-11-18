using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day24
{
    internal struct HexCoord
    {
        private static readonly Regex pathTemplate = new Regex(@"^(e|w|ne|nw|se|sw)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly HexCoord[] neighbors = new HexCoord[]
            {
                new HexCoord(+1, -1, 0), new HexCoord(+1, 0, -1), new HexCoord(0, +1, -1), 
                new HexCoord(-1, +1, 0), new HexCoord(-1, 0, +1), new HexCoord(0, -1, +1), 
            };

        public int X { get;}
        public int Y { get;}
        public int Z { get;}

        public HexCoord(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public HexCoord MoveEast(int distance)
        {
            return new HexCoord(X + distance, Y - distance, Z);
        }

        public HexCoord MoveNorthEast(int distance)
        {
            return new HexCoord(X + distance, Y, Z - distance);
        }

        public HexCoord MoveNorthWest(int distance)
        {
            return new HexCoord(X, Y + distance, Z - distance);
        }

        public IEnumerable<HexCoord> Neighbors()
        {
            var me = this;
            return neighbors.Select(n => new HexCoord(me.X + n.X, me.Y + n.Y, me.Z + n.Z));
        }

        public static HexCoord Parse(string path)
        {
            var m = pathTemplate.Match(path);

            if(!m.Success)
            {
                throw new ArgumentException("Invalid path string.");
            }

            var result = new HexCoord();
            foreach (Capture move in m.Groups[1].Captures)
            {
                switch (move.Value)
                {
                    case "e":
                        result = result.MoveEast(1);
                        break;
                    case "w":
                        result = result.MoveEast(-1);
                        break;
                    case "nw":
                        result = result.MoveNorthWest(1);
                        break;
                    case "se":
                        result = result.MoveNorthWest(-1);
                        break;
                    case "ne":
                        result = result.MoveNorthEast(1);
                        break;
                    case "sw":
                        result = result.MoveNorthEast(-1);
                        break;
                    default:
                        break;  
                }
            }

            if(result.X + result.Y + result.Z != 0)
            {
                throw new Exception("Broken coordinates.");
            }

            return result;
        }
    }
}
