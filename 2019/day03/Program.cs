using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            Wire[] wires = PuzzleInput.Select(moves => Wire.FromMoves(moves)).ToArray();
            var intersectionPoints = wires[0].Intersect(wires[1]);
            Point2D result = intersectionPoints.OrderBy(p => Manhattan(p)).First();

            Console.WriteLine(result);
            Console.WriteLine(Manhattan(result));

            Console.WriteLine("day03 completed.");
        }

        private static object Manhattan(Point2D p)
        {
            return Math.Abs(p.X) + Math.Abs(p.Y);
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day03.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day03.Input.PuzzleInput.txt");
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
