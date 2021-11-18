using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace day20
{
    internal class Tiling
    {
        private List<Tile> tiles;
        private int size;

        public Tiling(int size, Tile firstTile)
        {
            this.size = size;
            this.tiles = new List<Tile>(1);
            this.tiles.Add(firstTile);
        }

        public Tiling(int size, int count, IEnumerable<Tile> tiles)
        {
            this.size = size;
            this.tiles = new List<Tile>(count);
            this.tiles.AddRange(tiles);
        }

        public bool IsComplete 
        { 
            get
            {
                return tiles.Count == size * size;
            } 
        }

        internal TileFamily ToTileFamily()
        {
            var newEast = new StringBuilder();
            var newNorth = new StringBuilder();
            var newWest = new StringBuilder();
            var newSouth = new StringBuilder();
            
            for (int i = 0; i < size; i++)
            {
                newEast.Append(tiles[i * size + size - 1].East);
                newNorth.Append(tiles[i].North);
                newWest.Append(tiles[i * size].West);
                newSouth.Append(tiles[(size - 1) * size + i].South);
            }

            var newContent = new StringBuilder();

            for (int majorRow = 0; majorRow < size; majorRow++)
            {
                for (int lineIndex = 0; lineIndex < tiles[0].Size; lineIndex++)
                {
                    for (int majorCol = 0; majorCol < size; majorCol++)
                    {
                        newContent.Append(tiles[majorRow * size + majorCol].GetContentLine(lineIndex));
                    }
                    newContent.AppendLine();
                }
            }

            return new TileFamily(Id, newEast.ToString(), newNorth.ToString(), newWest.ToString(), newSouth.ToString(), newContent.ToString());
        }

        public long Id 
        { 
            get
            {
                return (long)tiles[0].Id * tiles[size - 1].Id * tiles[tiles.Count - size].Id * tiles[tiles.Count - 1].Id;
            }
        }

        internal static Tiling SearchForTiling(IEnumerable<TileFamily> tileFamilies)
        {
            var tilingSize = (int)Math.Round(Math.Sqrt(tileFamilies.Count()));
            Stack<Tiling> searchBoundary = new Stack<Tiling>(tileFamilies.SelectMany(tf => tf.GenerateTiles()).Select(t => new Tiling(tilingSize, t)));

            while (searchBoundary.Count > 0)
            {
                var solutionCandidate = searchBoundary.Pop();
                
                if(solutionCandidate.IsComplete)
                {
                    return solutionCandidate;
                }

                foreach (var t in solutionCandidate.GeneratePossibleNextStates(tileFamilies))
                {
                    searchBoundary.Push(t);
                }
            }

            return default(Tiling);
        }

        private IEnumerable<Tiling> GeneratePossibleNextStates(IEnumerable<TileFamily> tileFamilies)
        {
            HashSet<long> usedTileIds = new HashSet<long>(tiles.Select(t => t.Id));

            foreach (var t in tileFamilies.Where(tf => !usedTileIds.Contains(tf.Id)).SelectMany(tf => tf.GenerateTiles()))
            {
                if(CanFit(t))
                {
                    yield return new Tiling(size, tiles.Count + 1, tiles.Append(t));
                }
            }
        }

        private bool CanFit(Tile t)
        {
            int nextPosition = tiles.Count;
            int x = nextPosition % size;
            int y = nextPosition / size;

            if(x > 0)
            {
                if(!tiles[nextPosition - 1].JoinsEast(t))
                {
                    return false;
                }
            }

            if(y > 0)
            {
                if(!tiles[nextPosition - size].JoinsSouth(t))
                {
                    return false;
                }
            }

            return true;
        }

        public Tiling FlipH()
        {
            var flippedTiles = new List<Tile>(tiles.Count);

            for (int i = 0; i < tiles.Count; i++)
            {
                var sourceRow = i / size;
                var sourceCol = size - (i % size) - 1;
                var sourceIndex = size * sourceRow + sourceCol;
                flippedTiles.Add(tiles[sourceIndex].FlipH());
            }

            var res = new Tiling(size, tiles.Count, flippedTiles);
            return res;
        }

        public Tiling FlipV()
        {
            var flippedTiles = new List<Tile>(tiles.Count);

            for (int i = 0; i < tiles.Count; i++)
            {
                var sourceRow = size - (i / size) - 1;
                var sourceCol = i % size;
                var sourceIndex = size * sourceRow + sourceCol;
                flippedTiles.Add(tiles[sourceIndex].FlipV());
            }

            var res = new Tiling(size, tiles.Count, flippedTiles);
            return res;
        }

        public Tiling Rotate()
        {
            var rotatedTiles = new List<Tile>(tiles.Count);

            for (int i = 0; i < tiles.Count; i++)
            {
                var sourceRow = i % size;
                var sourceCol = size - (i / size) - 1;
                var sourceIndex = size * sourceRow + sourceCol;
                rotatedTiles.Add(tiles[sourceIndex].Rotate());
            }

            var res = new Tiling(size, tiles.Count, rotatedTiles);
            return res;
        }

        public override string ToString()
        {
            var outBldr = new StringBuilder();
            for (int i = 0; i < tiles.Count; i++)
            {
                outBldr.Append(tiles[i].Id);
                if((i + 1) % size == 0)
                {
                    outBldr.Append(Environment.NewLine);
                }
                else
                {
                    outBldr.Append(" ");
                }
            }
            return outBldr.ToString();
        }
    }
}