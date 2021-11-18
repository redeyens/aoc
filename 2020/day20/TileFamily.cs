using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace day20
{
    internal class TileFamily
    {
        private long id;
        private string east;
        private string north;
        private string west;
        private string south;
        private string content;

        public TileFamily(long id, string east, string north, string west, string south, string content)
        {
            this.id = id;
            this.east = east;
            this.north = north;
            this.west = west;
            this.south = south;
            this.content = content;
        }

        public long Id => id;

        public IEnumerable<Tile> GenerateTiles()
        {
            var tile = new Tile(id, east, north, west, south, content);

            yield return tile.FlipH();
            yield return tile.FlipV();
            yield return tile.FlipV().FlipH(); // Flip HV

            var tileRotated = tile.Rotate();

            yield return tileRotated.FlipH(); // Flip H + R
            yield return tileRotated.FlipV(); // Flip V + R
            yield return tileRotated.FlipV().FlipH(); // Flip HV + R

            yield return tileRotated;
            yield return tile;
        }

    }
}
