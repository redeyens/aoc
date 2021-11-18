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
            var componentsMass = PuzzleInput
                .Select(line => long.Parse(line))
                .Select(cm => GetFuelMass(cm).Sum())
                .ToList();
            

            Console.WriteLine(componentsMass.Sum());

            Console.WriteLine("day01 completed.");
        }

        private static IEnumerable<long> GetFuelMass(long startingMass)
        {
            long currentMass = startingMass;
            while ((currentMass = currentMass / 3 - 2) > 0)
            {
                 yield return currentMass;
            }
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
