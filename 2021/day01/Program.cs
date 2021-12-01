using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var readings = PuzzleInput.Select(line => int.Parse(line));
            int[] window = new int[3];
            int pos = 0;
            int depthIncreases = 0;

            foreach (var reading in readings)
            {
                if(pos / window.Length > 0 && reading > window[pos % window.Length])
                {
                    depthIncreases++;
                }
                window[pos++ % window.Length] = reading;
            }

            Console.WriteLine(depthIncreases);

            Console.WriteLine("day01 completed.");
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day01.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day01.Input.PuzzleInput.txt");
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
