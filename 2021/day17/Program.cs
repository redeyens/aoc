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

            int solutions = 0;

            for (int vY = Math.Abs(yRange[0]); vY >= yRange[0]; vY--)
            {
                for (int vX = 0; vX <= xRange[1]; vX++)
                {
                    if(IsHit((vX, vY), xRange, yRange))
                    {
                        solutions++;
                    }
                }
            }

            Console.WriteLine(solutions);

            Console.WriteLine("day17 completed.");
        }

        private static bool IsHit((int x, int y) velocity, int[] xRange, int[] yRange)
        {
            (int x, int y) position = (0, 0);
            (int x, int y) nextPosition = velocity;

            while (nextPosition.x <= xRange[1] && nextPosition.y >= yRange[0])
            {
                 position = nextPosition;
                 velocity.x = velocity.x - ((velocity.x > 0) ? 1 : (velocity.x < 0) ? -1 : 0);
                 velocity.y -= 1;
                 nextPosition.x = position.x + velocity.x;
                 nextPosition.y = position.y + velocity.y;
            }

            int xDist = (position.x < xRange[0]) ? xRange[0] - position.x : 0;
            int yDist = (position.y > yRange[1]) ? position.y - yRange[1] : 0;

            return (xDist + yDist) == 0;
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
