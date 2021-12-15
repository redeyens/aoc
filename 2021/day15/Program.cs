using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = PuzzleInput.ToArray();
            var goal = (x:map[0].Length * 5 - 1, y:map.Length * 5 - 1);
            var nextNodes = new Dictionary<(int x, int y), int>();
            var visitedNodes = new Dictionary<(int x, int y), int>();

            nextNodes.Add((0,0), 0);

            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.OrderBy(n => n.Value).First();
                nextNodes.Remove(currentNode.Key);

                if(currentNode.Key == goal)
                {
                    Console.WriteLine(currentNode.Value);
                    break;
                }

                visitedNodes[currentNode.Key] = currentNode.Value;
                foreach (var nextMove in GetPossibleMoves(currentNode.Key, map).Where(nm => !visitedNodes.ContainsKey(nm)))
                {
                    int prevRiskToNext = 0;
                    if (!nextNodes.TryGetValue(nextMove, out prevRiskToNext))
                    {
                        prevRiskToNext = int.MaxValue;
                    }

                    int riskToNext = currentNode.Value + GetNextRisk(map, nextMove);
                    if (riskToNext < prevRiskToNext)
                    {
                        nextNodes[nextMove] = riskToNext;
                    }
                }
            }

            Console.WriteLine("day15 completed.");
        }

        private static int GetNextRisk(string[] map, (int x, int y) nextMove)
        {
            int localY = nextMove.y % map.Length;
            int localX = nextMove.x % map[localY].Length;
            int baseRisk = map[localY][localX] - '0';
            baseRisk += nextMove.y / map.Length + nextMove.x / map[localY].Length;
            return (baseRisk - 1) % 9 + 1;
        }

        private static IEnumerable<(int x, int y)> GetPossibleMoves((int x, int y) pos, string[] map)
        {
            if(pos.x > 0)
            {
                yield return (pos.x - 1, pos.y);
            }
            if(pos.y > 0)
            {
                yield return (pos.x, pos.y - 1);
            }
            if(pos.x < (map[pos.y % map.Length].Length * 5 - 1))
            {
                yield return (pos.x + 1, pos.y);
            }
            if(pos.y < (map.Length * 5 - 1))
            {
                yield return (pos.x, pos.y + 1);
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day15.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day15.Input.PuzzleInput.txt");
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
