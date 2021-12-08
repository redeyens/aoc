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

            string segmentsInClass2 = digitClasses[2].First();
            segmentCandidates['c'] = new HashSet<char>(segmentsInClass2);
            segmentCandidates['f'] = new HashSet<char>(segmentsInClass2);

            string segmentsInClass3 = digitClasses[3].First();
            segmentCandidates['a'] = new HashSet<char>(segmentsInClass3.Where(c => !segmentCandidates['c'].Contains(c) && !segmentCandidates['f'].Contains(c)));

            string segmentsInClass4 = digitClasses[4].First();
            var newSegmentsInClass4 = segmentsInClass4.Where(c => !segmentCandidates['c'].Contains(c) && !segmentCandidates['f'].Contains(c));
            segmentCandidates['b'] = new HashSet<char>(newSegmentsInClass4);
            segmentCandidates['d'] = new HashSet<char>(newSegmentsInClass4);

            // 5 - 2, 3, 5 
            var newSegmentsFromClass5 = digitClasses[5]
                            .SelectMany(d => d)
                            .Where(c => !segmentCandidates['c'].Contains(c)
                                     && !segmentCandidates['f'].Contains(c)
                                     && !segmentCandidates['a'].Contains(c)
                                     && !segmentCandidates['b'].Contains(c)
                                     && !segmentCandidates['d'].Contains(c))
                            .GroupBy(c => c);
            var allThreeHaveG = newSegmentsFromClass5.Where(g => g.Count() == 3)
                            .Select(g => g.Key);
            segmentCandidates['g'] = new HashSet<char>(allThreeHaveG);

            var onlyOneHasE = newSegmentsFromClass5.Where(g => g.Count() == 1)
                            .Select(g => g.Key);
            segmentCandidates['e'] = new HashSet<char>(onlyOneHasE);

            // 6 - 0, 6, 9
            var cdeSegments = digitClasses[6]
                    .SelectMany(digit => digit)
                    .GroupBy(segment => segment)
                    .Where(segmentGroup => segmentGroup.Count() == 2)
                    .Select(segmentGroup => segmentGroup.Key)
                    .ToHashSet();

            segmentCandidates['c'].IntersectWith(cdeSegments);
            segmentCandidates['f'].ExceptWith(segmentCandidates['c']);

            segmentCandidates['d'].IntersectWith(cdeSegments);
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