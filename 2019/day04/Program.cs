using System;
using System.Collections.Generic;
using System.IO;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            int min = 147981;
            int max = 691423;

            int candidates = 0;

            for (int i = min; i <= max; i++)
            {
                int[] digits = GetDigits(i);
                if(HasDoubleDigits(digits) && IsNonDecreasing(digits))
                {
                    candidates++;
                }
            }

            Console.WriteLine(candidates);

            Console.WriteLine("day04 completed.");
        }

        private static bool IsNonDecreasing(int[] digits)
        {
            for (int i = 1; i < digits.Length; i++)
            {
                if(digits[i - 1] < digits[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool HasDoubleDigits(int[] digits)
        {
            HashSet<int> digitsSeen = new HashSet<int>();
            digitsSeen.UnionWith(digits);
            
            return digitsSeen.Count < 6;
        }

        private static int[] GetDigits(int num)
        {
            int[] result = new int[6];
            int current = num;

            for (int i = 0; i < 6; i++)
            {
                result[i] = current % 10;
                current = current / 10;
            }

            return result;
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day04.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day04.Input.PuzzleInput.txt");
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
