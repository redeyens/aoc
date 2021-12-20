using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day20
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = PuzzleInput;
            var pixelMap = input.First().Select(c => c == '.' ? 0 : 1).ToArray();

            var image = input.Skip(2)
                .Select((l, r) => (r, l))
                .SelectMany(row => row.l.Select((p, c) => (coord:(row:row.r, col:c), p)).Where(pixel => pixel.p == '#'))
                .Select(pixel => pixel.coord)
                .ToHashSet();
            
            image = Enahance(image, pixelMap, 0);
            image = Enahance(image, pixelMap, 1);

            Console.WriteLine(image.Count);

            Console.WriteLine("day20 completed.");
        }

        private static HashSet<(int row, int col)> Enahance(HashSet<(int row, int col)> image, int[] pixelMap, int def)
        {
            int minRow = image.Select(coord => coord.row).Min();
            int maxRow = image.Select(coord => coord.row).Max();
            int minCol = image.Select(coord => coord.col).Min();
            int maxCol = image.Select(coord => coord.col).Max();

            var imageBounds = (minRow, maxRow, minCol, maxCol);

            return GetPossiblePixels(image, imageBounds)
                .Select(coord => (coord, p:Lookup(coord, image, pixelMap, def, imageBounds)))
                .Where(pixel => pixel.p == 1)
                .Select(pixel => pixel.coord)
                .ToHashSet();
        }

        private static IEnumerable<(int row, int col)> GetPossiblePixels(HashSet<(int row, int col)> image, (int minRow, int maxRow, int minCol, int maxCol) imageBounds)
        {
            for (int row = imageBounds.minRow - 1; row < imageBounds.maxRow + 2; row++)
            {
                for (int col = imageBounds.minCol - 1; col < imageBounds.maxCol + 2; col++)
                {
                    yield return (row, col);
                }
            }
        }

        private static int Lookup((int row, int col) coord, HashSet<(int row, int col)> image, int[] pixelMap, int def, (int minRow, int maxRow, int minCol, int maxCol) imageBounds)
        {
            int index = 0;
            for (int dr = -1; dr < 2; dr++)
            {
                for (int dc = -1; dc < 2; dc++)
                {
                    var cp = (coord.row + dr, coord.col + dc);
                    int p = Out(cp, imageBounds) ? def : ((image.Contains(cp) ? 1 : 0));
                    index = index << 1 | p;
                }
            }

            return pixelMap[index];
        }

        private static bool Out((int r, int c) coord, (int minRow, int maxRow, int minCol, int maxCol) imageBounds)
        {
            return coord.r < imageBounds.minRow 
                || coord.r > imageBounds.maxRow
                || coord.c < imageBounds.minCol
                || coord.c > imageBounds.maxCol;
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day20.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day20.Input.PuzzleInput.txt");
            }
        }

        private static IEnumerable<string> GetLinesFromResource(string name)
        {
            using (Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                using (TextReader inReader = new StreamReader(inStream))
                {
                    string line;
                    while ((line = inReader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}
