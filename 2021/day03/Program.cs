using System;
using System.Collections.Generic;
using System.IO;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] countOnes = null;
            int sampleCount = 0;

            foreach (string line in PuzzleInput)
            {
                if(countOnes == null)
                {
                    countOnes = new int[line.Length];
                }

                for (int i = 0; i < line.Length; i++)
                {
                    countOnes[i] += line[i] - '0';
                }

                sampleCount++;
            }

            int gammaRate = 0;
            int epsilonRate = 0;

            for (int i = 0; i < countOnes.Length; i++)
            {
                gammaRate <<= 1;
                epsilonRate <<= 1;

                if(countOnes[i] > (sampleCount / 2))
                {
                    gammaRate |= 1;
                }
                else
                {
                    epsilonRate |= 1;
                }
            }

            Console.WriteLine(gammaRate * epsilonRate);

            Console.WriteLine("day03 completed.");
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day03.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day03.Input.PuzzleInput.txt");
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
