using System;
using System.Collections.Generic;
using System.Linq;

namespace day08
{
    internal class SeventSegDisplay
    {
        private string[] pattern;
        private string[] output;
        private Dictionary<char, char> mapping;
        private static readonly Dictionary<string, int> digits = new Dictionary<string, int>()
        {
            {"abcefg", 0},
            {"cf", 1},
            {"acdeg", 2},
            {"acdfg", 3},
            {"bcdf", 4},
            {"abdfg", 5},
            {"abdefg", 6},
            {"acf", 7},
            {"abcdefg", 8},
            {"abcdfg", 9},
        };

        private SeventSegDisplay(string[] pattern, string[] output)
        {
            this.pattern = pattern;
            this.output = output;
            Decode();
        }

        public string[] Output => output;

        public int DecodedOutput => Decode(Output);

        private int Decode(string[] display)
        {  
            return GetDigits(display).Aggregate(0, (res, digit) => res = res * 10 + digit);
        }

        private IEnumerable<int> GetDigits(string[] display)
        {
            return display.Select(digitString => digits[new string(digitString.Select(c => mapping[c]).OrderBy(c => c).ToArray())]);
        }

        private void Decode()
        {
            Dictionary<char, HashSet<char>> segmentCandidates = new Dictionary<char, HashSet<char>>();

            var digitClasses = pattern.GroupBy(d => d.Length).ToDictionary(g => g.Key, g => g.ToList());

            string digit = digitClasses[2].First();
            segmentCandidates['c'] = new HashSet<char>(digit);
            segmentCandidates['f'] = new HashSet<char>(digit);

            digit = digitClasses[3].First();
            segmentCandidates['a'] = new HashSet<char>(digit.Where(c => !segmentCandidates['c'].Contains(c) && !segmentCandidates['f'].Contains(c)));

            digit = digitClasses[4].First();
            var remaining = digit.Where(c => !segmentCandidates['c'].Contains(c) && !segmentCandidates['f'].Contains(c));
            segmentCandidates['b'] = new HashSet<char>(remaining);
            segmentCandidates['d'] = new HashSet<char>(remaining);

            // 5 - 2, 3, 5 
            var grouping4 = digitClasses[5]
                            .SelectMany(d => d)
                            .Where(c => !segmentCandidates['c'].Contains(c)
                                     && !segmentCandidates['f'].Contains(c)
                                     && !segmentCandidates['a'].Contains(c)
                                     && !segmentCandidates['b'].Contains(c)
                                     && !segmentCandidates['d'].Contains(c))
                            .GroupBy(c => c);
            remaining = grouping4.Where(g => g.Count() == 3)
                            .Select(g => g.Key);
            segmentCandidates['g'] = new HashSet<char>(remaining);

            remaining = grouping4.Where(g => g.Count() == 1)
                            .Select(g => g.Key);
            segmentCandidates['e'] = new HashSet<char>(remaining);

            // 6 - 0, 6, 9
            var cde = digitClasses[6]
                    .SelectMany(d => d)
                    .GroupBy(c => c)
                    .Where(g => g.Count() == 2)
                    .Select(g => g.Key)
                    .ToHashSet();

            segmentCandidates['c'].IntersectWith(cde);
            segmentCandidates['f'].ExceptWith(segmentCandidates['c']);

            segmentCandidates['d'].IntersectWith(cde);
            segmentCandidates['b'].ExceptWith(segmentCandidates['d']);

            mapping = segmentCandidates.ToDictionary(kvp => kvp.Value.First(), kvp => kvp.Key);
        }

        internal static SeventSegDisplay FromString(string line)
        {
            string[] sequences = line.Split('|');
            string[] pattern = sequences[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] output = sequences[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return new SeventSegDisplay(pattern, output);
        }
    }
}