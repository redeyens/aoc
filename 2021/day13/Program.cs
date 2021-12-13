using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputBlocks = PuzzleInputText.Split(Environment.NewLine + Environment.NewLine);

            var dots = inputBlocks[0].Split(Environment.NewLine).Select(line => {var coord = line.Split(','); return (x:int.Parse(coord[0]), y:int.Parse(coord[1]));}).ToHashSet();

            var folds = inputBlocks[1].Split(Environment.NewLine).Select(line => line.Split(' ')[2]).Select(fold => {var p = fold.Split('='); return (axis:p[0], line:int.Parse(p[1]));});

            Fold(dots, folds.First());

            Console.WriteLine(dots.Count);

            Console.WriteLine("day13 completed.");
        }

        private static void Fold(HashSet<(int x, int y)> dots, (string axis, int line) foldLine)
        {
            if(foldLine.axis.Equals("x"))
            {
                FoldLeft(dots, foldLine.line);
            }
            else
            {
                FoldUp(dots, foldLine.line);
            }
        }

        private static void FoldUp(HashSet<(int x, int y)> dots, int line)
        {
            var foldedDots = dots.Where(d => d.y > line).ToList();
            dots.ExceptWith(foldedDots);
            dots.UnionWith(foldedDots.Select(d => (x:d.x, y:2 * line - d.y)));
        }

        private static void FoldLeft(HashSet<(int x, int y)> dots, int line)
        {
            var foldedDots = dots.Where(d => d.x > line).ToList();
            dots.ExceptWith(foldedDots);
            dots.UnionWith(foldedDots.Select(d => (x:2 * line - d.x, y:d.y)));
        }

        private static string TestInputText
        {
            get
            {
                return GetTextFromResource("day13.Input.TestInput.txt");
            }
        }

        private static string PuzzleInputText
        {
            get
            {
                return GetTextFromResource("day13.Input.PuzzleInput.txt");
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day13.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day13.Input.PuzzleInput.txt");
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

        private static string GetTextFromResource(string name)
        {
            using (Stream inStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                using (TextReader inReader = new StreamReader(inStream))
                {
                    return inReader.ReadToEnd();
                }
            }
        }
    }
}
