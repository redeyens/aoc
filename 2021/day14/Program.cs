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
            var polymer = PuzzleInput.First().ToList();
            var insertions = PuzzleInput.Skip(2).Select(i => i.Split(" -> ")).ToDictionary(p => p[0], p => p[1][0]);
            var pendingInsertions = new Stack<(int pos, char element)>();
            var pair = new char[2];

            for (int step = 0; step < 40; step++)
            {
                for (int i = 1; i < polymer.Count; i++)
                {
                    char insertedElement = default(char);
                    pair[0] = polymer[i - 1];
                    pair[1] = polymer[i];

                    if(insertions.TryGetValue(new string(pair), out insertedElement))
                    {
                        pendingInsertions.Push((i, insertedElement));
                    }
                }

                while (pendingInsertions.Count >0)
                {
                    var ins = pendingInsertions.Pop();
                    polymer.Insert(ins.pos, ins.element);
                }
            }

            var sortedElements = polymer.GroupBy(c => c).Select(g => g.Count()).OrderBy(n => n).ToList();

            Console.WriteLine(sortedElements[sortedElements.Count - 1] - sortedElements[0]);

            Console.WriteLine("day14 completed.");
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
