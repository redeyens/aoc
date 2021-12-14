using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var polymer = InitPairCount(PuzzleInput.First());
            var insertions = PuzzleInput.Skip(2).Select(i => i.Split(" -> ")).Select(s => (first:s[0][0], second:s[0][1], el:s[1][0])).ToList();
            var pendingPairs = new List<(char first, char second, long cnt)>();

            for (int step = 0; step < 40; step++)
            {
                pendingPairs.Clear();
                foreach (var ins in insertions)
                {
                    TouchEntry(polymer, ins.first, ins.second);

                    long pairCount = polymer[(ins.first, ins.second)];
                    pendingPairs.Add((ins.first, ins.second, -pairCount));
                    pendingPairs.Add((ins.first, ins.el, pairCount));
                    pendingPairs.Add((ins.el, ins.second, pairCount));
                }
                foreach (var pair in pendingPairs)
                {
                    polymer[(pair.first, pair.second)] += pair.cnt;
                }
            }

            var sortedElements = polymer
                .GroupBy(e => e.Key.second, e => e.Value)
                .Select(g => g.Sum())
                .OrderBy(c => c)
                .ToList();

            Console.WriteLine(sortedElements[sortedElements.Count - 1] - sortedElements[0]);

            Console.WriteLine("day14 completed.");
        }

        private static Dictionary<(char first, char second),long> InitPairCount(string startingPolymer)
        {
            var res = new Dictionary<(char first, char second),long>();

            res[(default(char), startingPolymer[0])] = 1;

            for (int i = 1; i < startingPolymer.Length; i++)
            {
                char firstChar = startingPolymer[i - 1];
                char secondChar = startingPolymer[i];

                TouchEntry(res, firstChar, secondChar);

                res[(firstChar, secondChar)] += 1;
            }

            return res;
        }

        private static void TouchEntry(Dictionary<(char first, char second),long> polymer, char firstChar, char secondChar)
        {
            if (!polymer.TryGetValue((firstChar, secondChar), out long cnt))
            {
                polymer[(firstChar, secondChar)] = 0;
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day14.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day14.Input.PuzzleInput.txt");
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
