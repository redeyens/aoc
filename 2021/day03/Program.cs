using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            int pos = 0;
            List<string> oxyRatingNumbers = PuzzleInput.ToList();
            List<string> scrubberRatingNumbers = PuzzleInput.ToList();

            while (oxyRatingNumbers.Count > 1 || scrubberRatingNumbers.Count > 1)
            {
                oxyRatingNumbers = oxyRatingNumbers.GroupBy(num => num[pos]).OrderByDescending(ComapreRatings()).First().ToList();

                scrubberRatingNumbers = scrubberRatingNumbers.GroupBy(num => num[pos]).OrderBy(ComapreRatings()).First().ToList();

                pos++;
            }

            int oxyRating = Convert.ToInt32(oxyRatingNumbers[0], 2);
            int scrubberRating = Convert.ToInt32(scrubberRatingNumbers[0], 2);

            Console.WriteLine(oxyRating * scrubberRating);

            Console.WriteLine("day03 completed.");
        }

        private static Func<IGrouping<char, string>, int> ComapreRatings()
        {
            return group => group.Count() * 10 + (group.Key - '0');
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
