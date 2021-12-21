using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day21
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = PuzzleInput
                .Select(l => l.Split(' ').Skip(4).Select(int.Parse).First() - 1)
                .ToArray();
            
            var score = new int[2];

            var diceRolls = DeterministicDie().GetEnumerator();

            int countRolls = 0;

            for (countRolls = 0; score[0] < 1000 && score[1] < 1000; countRolls++)
            {
                diceRolls.MoveNext();
                players[countRolls % 2] = (players[countRolls % 2] + diceRolls.Current) % 10;
                score[countRolls % 2] += players[countRolls % 2] + 1;
            }

            Console.WriteLine(score.Min() * countRolls * 3);

            Console.WriteLine("day21 completed.");
        }

        private static IEnumerable<int> DeterministicDie()
        {
            for (int i = 0;; i++)
            {
                yield return (i % 100 + 1) + (++i % 100 + 1) + (++i % 100 + 1);
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day21.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day21.Input.PuzzleInput.txt");
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
