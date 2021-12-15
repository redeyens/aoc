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
            var goal = (x:map[0].Length - 1, y:map.Length - 1);
            var riskMap = map.Select(row => row.Select(cell => int.MaxValue).ToArray()).ToArray();
            HashSet<(int x, int y)> nextNodes = new HashSet<(int x, int y)>();
            HashSet<(int x, int y)> visitedNodes = new HashSet<(int x, int y)>();

            nextNodes.Add((0,0));
            riskMap[0][0] = 0;

            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.OrderBy(n => riskMap[n.y][n.x] + EstimateRemainingRisk(n, goal)).First();
                nextNodes.Remove(currentNode);

                if(currentNode == goal)
                {
                    Console.WriteLine(riskMap[currentNode.y][currentNode.x]);
                    break;
                }

                visitedNodes.Add(currentNode);
                foreach (var nextMove in GetPossibleMoves(currentNode, map).Except(visitedNodes))
                {
                    int riskToNext = riskMap[currentNode.y][currentNode.x] + map[nextMove.y][nextMove.x] - '0';
                    if(riskToNext < riskMap[nextMove.y][nextMove.x])
                    {
                        riskMap[nextMove.y][nextMove.x] = riskToNext;
                    }
                    nextNodes.Add(nextMove);
                }
            }

            Console.WriteLine("day15 completed.");
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
            if(pos.x < (map[pos.y].Length - 1))
            {
                yield return (pos.x + 1, pos.y);
            }
            if(pos.y < (map.Length - 1))
            {
                yield return (pos.x, pos.y + 1);
            }
        }

        private static int EstimateRemainingRisk((int x, int y) p, (int x, int y) goal)
        {
            return goal.x - p.x + goal.y + p.y;
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
