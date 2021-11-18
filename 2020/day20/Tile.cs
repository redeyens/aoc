using System;
using System.Linq;
using System.Text;

namespace day20
{
    public class Tile
    {
        private long id;
        private string east;
        private string north;
        private string west;
        private string south;
        private string[] content;

        public Tile(long id, string east, string north, string west, string south, string content)
        {
            this.id = id;
            this.east = east;
            this.north = north;
            this.west = west;
            this.south = south;
            this.content = content.Split(Environment.NewLine).Where(l => !string.IsNullOrEmpty(l)).ToArray();
        }

        public long Id => id;

        public int Size => content.Length;

        public string East => east;

        public string North => north;

        public string West => west;

        public string South => south;

        internal bool JoinsEast(Tile t)
        {
            return east == t.west;
        }

        internal bool JoinsSouth(Tile t)
        {
            return south == t.north;
        }

        public override string ToString()
        {
            var lineArray = north.ToCharArray();
            var resBldr = new StringBuilder();

            resBldr.AppendLine(north);
            for (int i = 1; i < east.Length - 1; i++)
            {
                lineArray[0] = west[i];
                for (int j = 1; j < lineArray.Length - 1; j++)
                {
                    lineArray[j] = ' ';
                }
                lineArray[lineArray.Length - 1] = east[i];

                resBldr.AppendLine(new string(lineArray));
            }
            resBldr.AppendLine(south);

            return resBldr.ToString();
        }

        internal void RemovePixel(int line, int c)
        {
            var editedLine = content[line].ToCharArray();
            editedLine[c] = '.';
            content[line] = new string(editedLine);
        }

        internal Tile Rotate()
        {
            var rNorth = east;
            var rSouth = west;
            var rEast = Reverse(south);
            var rWest = Reverse(north);

            return new Tile(id, rEast, rNorth, rWest, rSouth, Rotate(content)); // R
        }

        public string GetContentString()
        {
            return string.Join(Environment.NewLine, content);
        }

        internal string GetContentLine(int lineIndex)
        {
            return content[lineIndex];
        }

        internal Tile FlipH()
        {
            var revNorth = Reverse(north);
            var revSouth = Reverse(south);

            return new Tile(id, west, revNorth, east, revSouth, FlipContentH(content)); // Flip H
        }

        internal Tile FlipV()
        {
            var revEast = Reverse(east);
            var revWest = Reverse(west);

            return new Tile(id, revEast, south, revWest, north, FlipContentV(content)); // Flip V
        }

        private string FlipContentH(string[] content)
        {
            var outBldr = new StringBuilder();
            foreach (var line in content)
            {
                outBldr.AppendLine(Reverse(line));
            }

            return outBldr.ToString();
        }

        private string Reverse(string line)
        {
            var array = line.ToCharArray();
            Array.Reverse(array);
            var rEast = new string(array);
            return rEast;
        }

        private string FlipContentV(string[] content)
        {
            var outBldr = new StringBuilder();
            foreach (var line in content.Reverse())
            {
                outBldr.AppendLine(line);
            }

            return outBldr.ToString();
        }

        private string Rotate(string[] content)
        {
            var contArray = content;
            var currentLine = new char[contArray.Length];
            var lastIndex = contArray.Length - 1;
            var outBldr = new StringBuilder();

            for (int rowIndex = 0; rowIndex < contArray.Length; rowIndex++)
            {
                var sourceColIndex = lastIndex - rowIndex;
                for (int colIndex = 0; colIndex < contArray.Length; colIndex++)
                {
                    var sourceRowIndex = colIndex;
                    currentLine[colIndex] = contArray[sourceRowIndex][sourceColIndex];
                }
                outBldr.AppendLine(new string(currentLine));
            }

            return outBldr.ToString();
        }

    }
}