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
            var polymer = GetPairs(PuzzleInput.First());
            var insertions = PuzzleInput.Skip(2).Select(i => i.Split(" -> ")).Select(s => (pair:s[0], el:s[1][0])).ToList();
            var pendingPairs = new List<(char first, char second, long cnt)>();

            for (int step = 0; step < 40; step++)
            {
                pendingPairs.Clear();
                foreach (var ins in insertions)
                {
                    TouchEntry(polymer, ins.pair[0], ins.pair[1]);

                    long pairCount = polymer[ins.pair[0]][ins.pair[1]];
                    pendingPairs.Add((ins.pair[0], ins.pair[1], -pairCount));
                    pendingPairs.Add((ins.pair[0], ins.el, pairCount));
                    pendingPairs.Add((ins.el, ins.pair[1], pairCount));
                }
                foreach (var pair in pendingPairs)
                {
                    polymer[pair.first][pair.second] += pair.cnt;
                }
            }

            var sortedElements = polymer.Values
                .SelectMany(s => s.Select(e => (c:e.Key, cnt:e.Value)))
                .GroupBy(e => e.c, e => e.cnt)
                .Select(g => g.Sum())
                .OrderBy(c => c)
                .ToList();

            Console.WriteLine(sortedElements[sortedElements.Count - 1] - sortedElements[0]);

            Console.WriteLine("day14 completed.");
        }

        private static Dictionary<char,Dictionary<char,long>> GetPairs(string startingPolymer)
        {
            var res = new Dictionary<char,Dictionary<char,long>>();

            res[default(char)] = new Dictionary<char, long>();
            res[default(char)][startingPolymer[0]] = 1;

            for (int i = 1; i < startingPolymer.Length; i++)
            {
                char firstChar = startingPolymer[i - 1];
                char secondChar = startingPolymer[i];

                TouchEntry(res, firstChar, secondChar);

                res[firstChar][secondChar] += 1;
            }

            return res;
        }

        private static void TouchEntry(Dictionary<char, Dictionary<char, long>> polymer, char firstChar, char secondChar)
        {
            Dictionary<char, long> second;
            if (!polymer.TryGetValue(firstChar, out second))
            {
                second = new Dictionary<char, long>();
                polymer[firstChar] = second;
            }

            if (!second.TryGetValue(secondChar, out long cnt))
            {
                second[secondChar] = 0;
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
