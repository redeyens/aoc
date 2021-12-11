using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day11
{
    class Program
    {
        static void Main(string[] args)
        {
            int flashes = 0;
            var state = PuzzleInput.Select(line => line.Select(c => c - '0').ToArray()).ToArray();

            for (int i = 0; i < 100; i++)
            {
                flashes += AdvanceOneStep(state);
            }

            Console.WriteLine(flashes);

            Console.WriteLine("day11 completed.");
        }

        private static void PrintState(int[][] state)
        {
            Console.WriteLine(string.Join(Environment.NewLine, state.Select(row => new string(row.Select(c => (char)(c + '0')).ToArray()))));
        }

        private static int AdvanceOneStep(int[][] state)
        {
            HashSet<(int i, int j)> activatedInThisStep = new HashSet<(int i, int j)>();

            var activatedNext = AdvanceAll(state).ToHashSet();

            while (activatedNext.Count > 0)
            {
                 activatedInThisStep.UnionWith(activatedNext);

                 activatedNext = activatedNext.SelectMany(coord => AdvanceAdjacent(state, coord.i, coord.j)).ToHashSet();

                 activatedNext.ExceptWith(activatedInThisStep);
            }

            ResetFlashed(state, activatedInThisStep);

            return activatedInThisStep.Count;
        }

        private static void ResetFlashed(int[][] state, HashSet<(int i, int j)> activatedInThisStep)
        {
            foreach (var coord in activatedInThisStep)
            {
                state[coord.i][coord.j] = 0;
            }
        }

        private static IEnumerable<(int i, int j)> AdvanceAdjacent(int[][] state, int i, int j)
        {
            for (int di = -1; di < 2; di++)
            {
                for (int dj = -1; dj < 2; dj++)
                {
                    int x = i + di;
                    int y = j + dj;

                    if(x < 0 
                        || x == state.Length
                        || y < 0
                        || y == state[x].Length
                        || (di == 0 && dj == 0))
                    {
                        continue;
                    }

                    if (++state[x][y] > 9)
                    {
                        yield return (x, y);
                    }
                }
            }
        }

        private static IEnumerable<(int i, int j)> AdvanceAll(int[][] state)
        {
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    if(++state[i][j] > 9)
                    {
                        yield return (i, j);
                    }
                }
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day11.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day11.Input.PuzzleInput.txt");
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
