using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day08
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> uniqueDigits = new HashSet<int>() {2, 4, 3, 7};

            IEnumerable<int> allOutputValues = PuzzleInput
                            .Select(line => SeventSegDisplay.FromString(line))
                            .Select(d => d.DecodedOutput);
            
            Console.WriteLine(
                allOutputValues
                .Sum()
                );

            Console.WriteLine("day08 completed.");
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day08.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day08.Input.PuzzleInput.txt");
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
