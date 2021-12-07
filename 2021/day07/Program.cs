using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day07
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] crabs = PuzzleInput.First().Split(',').Select(int.Parse).OrderBy(n => n).ToArray();
            int average = 0;

            average = (int)Math.Round(crabs.Average());

            int fuelRq1 = crabs.Select(c => FuelRequired(c, average - 1)).Sum();
            int fuelRq2 = crabs.Select(c => FuelRequired(c, average)).Sum();
            int fuelRq3 = crabs.Select(c => FuelRequired(c, average + 1)).Sum();

            int fuelRq = Math.Min(Math.Min(fuelRq1, fuelRq2), fuelRq3);

            Console.WriteLine(fuelRq);

            Console.WriteLine("day07 completed.");
        }

        private static int FuelRequired(int x1, int x2)
        {
            int range = Math.Abs(x1 - x2);
            return (int)Math.Round((range * (range + 1)) / 2.0);
        }

        private static int GetMedian(int[] values)
        {
            int median;
            if (values.Length % 2 == 0)
            {
                median = (int)Math.Round((values[values.Length / 2 - 1] + values[values.Length / 2]) / 2.0);
            }
            else
            {
                median = values[values.Length / 2 + 1];
            }

            return median;
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day07.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day07.Input.PuzzleInput.txt");
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
