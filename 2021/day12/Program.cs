using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var caveSystem = PuzzleInput.SelectMany(link => SplitLink(link)).GroupBy(link =>link.start).ToDictionary(group =>group.Key, group => group.Select(entry => entry.end).ToList());

            Queue<List<string>> frontier = new Queue<List<string>>();
            int paths = 0;

            frontier.Enqueue(new List<string>() {"start"});

            while (frontier.Count > 0)
            {
                List<string> currentPath = frontier.Dequeue();
                string currentCave = currentPath[currentPath.Count - 1];
                
                if(currentCave.Equals("end"))
                {
                    paths++;
                    continue;
                }

                foreach (var cave in caveSystem[currentCave].Where(cave => char.IsUpper(cave[0]) || !currentPath.Contains(cave)))
                {
                    List<string> nextPath = new List<string>(currentPath.Count + 1);
                    nextPath.AddRange(currentPath);
                    nextPath.Add(cave);
                    frontier.Enqueue(nextPath);
                }
            }

            Console.WriteLine(paths);

            Console.WriteLine("day12 completed.");
        }

        private static IEnumerable<(string start, string end)> SplitLink(string link)
        {
            var ends = link.Split('-');
            yield return (ends[0], ends[1]);
            yield return (ends[1], ends[0]);
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day12.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day12.Input.PuzzleInput.txt");
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
