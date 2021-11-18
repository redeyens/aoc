using System;
using System.Collections.Generic;
using System.Linq;

namespace day20
{
    internal class Monster
    {
        private IEnumerable<(int i, int j, char c)> nodes;
        private readonly int width;
        private readonly int height;

        public Monster(IEnumerable<(int i, int j, char c)> nodes)
        {
            this.nodes = nodes.ToList();
            this.width = this.nodes.Max(n => n.j);
            this.height = this.nodes.Max(n => n.i);
        }

        internal IEnumerable<(Tile t, int line, int c)> FindIn(Tile tile)
        {
            var image =  tile.GetContentString().Split(Environment.NewLine);

            for (int hIndex = 0; hIndex < image[0].Length - width; hIndex++)
            {
                for (int vIndex = 0; vIndex < image.Length - height; vIndex++)
                {
                    if(IsMatch(image, hIndex, vIndex))
                    {
                        yield return (tile, vIndex, hIndex);
                    }
                }
            }
        }

        private bool IsMatch(string[] image, int hIndex, int vIndex)
        {
            foreach (var node in nodes)
            {
                if(image[vIndex + node.i][hIndex + node.j] != '#')
                {
                    return false;
                }
            }

            return true;
        }

        internal void RemoveAt(Tile t, int line, int c)
        {
            foreach (var node in nodes)
            {
                t.RemovePixel(line + node.i, c + node.j);
            }
        }
    }
}