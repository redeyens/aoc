using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputParts = PuzzleInput.First().Split(new char[] {' ', ',', '='}, StringSplitOptions.RemoveEmptyEntries);
            var xRange = inputParts[3].Split("..").Select(int.Parse).ToArray();
            var yRange = inputParts[5].Split("..").Select(int.Parse).ToArray();

            Console.WriteLine(string.Join(" ", string.Join("..", xRange), string.Join("..", yRange)));

            var nextNodes = new List<((int x, int y) v, int s)>();
            var visitedNodes = new HashSet<(int x, int y)>();
            (int x, int y) startingV = (x: 1, y: Math.Abs(yRange[0]));
            var currentSolution = (v: startingV, s: Score(startingV, xRange, yRange));

            int delta = 1;

            nextNodes.Add(currentSolution);

            while (nextNodes.Count > 0)
            {
                currentSolution = nextNodes.OrderByDescending(n => n.s).First();
                visitedNodes.Add(currentSolution.v);

                nextNodes = nextNodes.Concat(GetNextNodes(currentSolution.v, delta)
                    .Select(v => (v, s:Score(v, xRange, yRange))))
                    .Where(n => n.s >= currentSolution.s)
                    .Where(n => !visitedNodes.Contains(n.v))
                    .ToList();
                
                if(delta > 1)
                {
                    delta--;
                }
            }

            Console.WriteLine(currentSolution.s);

            Console.WriteLine("day17 completed.");
        }

        private static IEnumerable<(int, int)> GetNextNodes((int x, int y) velocity, int delta)
        {
            int minDx = velocity.x > 1 ? -1 : 0;
            int minDy = velocity.y > 0 ? -1 : 0;

            for (int dx = minDx; dx < 2; dx++)
            {
                for (int dy = minDy; dy < 2; dy++)
                {
                    if(dx == dy && dx == 0)
                    {
                        continue;
                    }
                    yield return (velocity.x + delta * dx, velocity.y + delta * dy);
                }
            }
        }

        private static int Score((int x, int y) velocity, int[] xRange, int[] yRange)
        {
            (int x, int y) position = (0, 0);
            (int x, int y) nextPosition = velocity;
            int maxY = position.y;

            while (nextPosition.x <= xRange[1] && nextPosition.y >= yRange[0])
            {
                 position = nextPosition;
                 maxY = Math.Max(maxY, position.y);
                 velocity.x = velocity.x - ((velocity.x > 0) ? 1 : (velocity.x < 0) ? -1 : 0);
                 velocity.y -= 1;
                 nextPosition.x = position.x + velocity.x;
                 nextPosition.y = position.y + velocity.y;
            }

            int xDist = (position.x < xRange[0]) ? xRange[0] - position.x : 0;
            int yDist = (position.y > yRange[1]) ? position.y - yRange[1] : 0;

            return (xDist + yDist) * -10000 + maxY;
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day17.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day17.Input.PuzzleInput.txt");
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
