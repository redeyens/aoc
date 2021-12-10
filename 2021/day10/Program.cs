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
            {')', 1},
            {']', 2},
            {'}', 3},
            {'>', 4}
        };

        private static readonly Dictionary<char, char> bracketPairs =  new Dictionary<char, char>()
        {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'}
        };

        static void Main(string[] args)
        {
            var scores = PuzzleInput.Select(line => CloseBrackets(line).Aggregate(0L, (res, next) => res = res * 5 + scoreMap[next])).Where(s => s > 0).OrderBy(s => s).ToList();

            Console.WriteLine(scores[scores.Count / 2]);

            Console.WriteLine("day10 completed.");
        }

        private static IEnumerable<char> CloseBrackets(string line)
        {
            var charStack = new Stack<char>();
            bool isValid = true;

            foreach (var nextBracket in line)
            {
                if(bracketPairs.Keys.Contains(nextBracket))
                {
                    charStack.Push(nextBracket);
                }
                else
                {
                    char lastBracket = charStack.Peek();
                    if(Math.Abs(nextBracket - lastBracket) > 2)
                    {
                        isValid = false;
                        break;
                    }
                    else
                    {
                        charStack.Pop();
                    }
                }
            }

            if(isValid)
            {
                while (charStack.Count > 0)
                {
                        yield return bracketPairs[charStack.Pop()];
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
