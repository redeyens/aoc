using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day09
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = PuzzleInput.ToArray();

            var result = GetLowPoints(input)
                .Select(coord => FloodBasin(input, coord.row, coord.col).Count())
                .OrderByDescending(b => b)
                .Take(3)
                .Aggregate(1, (prod, curr) => prod *= curr);
            
            Console.WriteLine(result);

            Console.WriteLine("day09 completed.");
        }

        private static IEnumerable<(int row, int col)> FloodBasin(string[] input, int row, int col)
        {
            HashSet<(int row, int col)> basin = new HashSet<(int row, int col)>();
            Queue<(int row, int col)> pointsToCheck = new Queue<(int row, int col)>();

            pointsToCheck.Enqueue((row, col));

            while (pointsToCheck.Count > 0)
            {
                var current = pointsToCheck.Dequeue();

                if(input[current.row][current.col] == '9' || basin.Contains(current))
                {
                    continue;
                }

                basin.Add((current.row, current.col));

                if(current.row > 0)
                {
                    pointsToCheck.Enqueue((current.row - 1, current.col));
                }
                if(current.row < (input.Length - 1))
                {
                    pointsToCheck.Enqueue((current.row + 1, current.col));
                }
                if(current.col > 0)
                {
                    pointsToCheck.Enqueue((current.row, current.col - 1));
                }
                if(current.col < (input[row].Length - 1))
                {
                    pointsToCheck.Enqueue((current.row, current.col + 1));
                }
            }

            return basin;
        }

        private static IEnumerable<(int row, int col)> GetLowPoints(string[] input)
        {
            for (int j = 0; j < input.Length; j++)
            {
                string line = input[j];
                for (int i = 0; i < line.Length; i++)
                {
                    if (i > 0 && line[i] >= line[i - 1])
                    {
                        continue;
                    }
                    if (i < (line.Length - 1) && line[i] >= line[i + 1])
                    {
                        continue;
                    }
                    if (j > 0 && line[i] >= input[j - 1][i])
                    {
                        continue;
                    }
                    if (j < (input.Length - 1) && line[i] >= input[j + 1][i])
                    {
                        continue;
                    }

                    yield return (j, i);
                }
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day09.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day09.Input.PuzzleInput.txt");
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
