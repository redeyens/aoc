using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day10
{
    class Program
    {
        private static readonly Dictionary<char, int> scoreMap = new Dictionary<char, int>()
        {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137}
        };

        private static readonly HashSet<char> openingBrackets = new HashSet<char>() {'(', '[', '{', '<'};

        static void Main(string[] args)
        {
            Console.WriteLine(PuzzleInput.SelectMany(line => GetIllegal(line)).GroupBy(c => c).Select(g => scoreMap[g.Key] * g.Count()).Sum());

            Console.WriteLine("day10 completed.");
        }

        private static IEnumerable<char> GetIllegal(string line)
        {
            var charStack = new Stack<char>();

            foreach (var nextBracket in line)
            {
                if(openingBrackets.Contains(nextBracket))
                {
                    charStack.Push(nextBracket);
                }
                else
                {
                    char lastBracket = charStack.Peek();
                    if(Math.Abs(nextBracket - lastBracket) > 2)
                    {
                        yield return nextBracket;
                        break;
                    }
                    else
                    {
                        charStack.Pop();
                    }
                }
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day10.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day10.Input.PuzzleInput.txt");
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
