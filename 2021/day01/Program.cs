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
            var readings = PuzzleInput.Select(line => int.Parse(line)).ToArray();
            int? previousWindow = null;
            int depthIncreases = 0;

            for (int i = 1; i < readings.Length - 1; i++)
            {
                int currentWindow = readings[i - 1] + readings[i] + readings[i + 1];
                if(previousWindow.HasValue && currentWindow > previousWindow.Value)
                {
                    depthIncreases++;
                }
                previousWindow = currentWindow;
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
