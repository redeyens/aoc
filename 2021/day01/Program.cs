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
            int? previousReading = null;
            int depthIncreases = 0;

            foreach (var meas in readings)
            {
                if(previousReading.HasValue && meas > previousReading.Value)
                {
                    depthIncreases++;
                }
                previousReading = meas;
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
