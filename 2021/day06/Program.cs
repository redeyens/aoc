using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day06
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] fishNumByAge = new long[9];

            var fishGroupedByAge = PuzzleInput.SelectMany(l => l.Split(',')).Select(int.Parse).GroupBy(age => age).Select(g => (age: g.Key, num: g.Count()));

            foreach (var fishAgeGroup in fishGroupedByAge)
            {
                fishNumByAge[fishAgeGroup.age] = fishAgeGroup.num;
            }

            for (int i = 0; i < 80; i++)
            {
                long spawningToday = fishNumByAge[0];
                for (int j = 1; j < 9; j++)
                {
                    fishNumByAge[j - 1] = fishNumByAge[j];
                }
                
                fishNumByAge[8] = spawningToday;
                fishNumByAge[6] += spawningToday;
            }

            Console.WriteLine(fishNumByAge.Sum());

            Console.WriteLine("day06 completed.");
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day06.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day06.Input.PuzzleInput.txt");
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
