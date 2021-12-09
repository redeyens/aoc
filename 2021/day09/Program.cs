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

            Console.WriteLine(GetLowPoints(input).Select(lp => 1 + lp).Sum());

            Console.WriteLine("day09 completed.");
        }

        private static IEnumerable<int> GetLowPoints(string[] input)
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

                    yield return line[i] - '0';
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
