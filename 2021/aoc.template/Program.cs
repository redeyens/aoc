using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.template
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string line in TestInput)
            {
                Console.WriteLine(line);
            }

            foreach (string line in PuzzleInput)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("aoc.template completed.");
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("aoc.template.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("aoc.template.Input.PuzzleInput.txt");
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
