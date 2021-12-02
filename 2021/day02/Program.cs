using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = PuzzleInput.Select(ParseCommand);

            Position currentPosition = new Position();

            foreach (var command in commands)
            {
                currentPosition = command(currentPosition);
            }

            Console.WriteLine(currentPosition.Depth * currentPosition.HPos);

            Console.WriteLine("day02 completed.");
        }

        private static Func<Position, Position> ParseCommand(string commandText)
        {
            string[] cmd = commandText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            switch (cmd[0][0])
            {
                case 'u':
                    return (p) => new Position(p.Depth - int.Parse(cmd[1]), p.HPos);
                case 'd':
                    return (p) => new Position(p.Depth + int.Parse(cmd[1]), p.HPos);
                case 'f':
                    return (p) => new Position(p.Depth, p.HPos + int.Parse(cmd[1]));
                default:
                    return (p) => {Console.WriteLine($"Invalid command '{commandText}'."); return p;};
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day02.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day02.Input.PuzzleInput.txt");
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
