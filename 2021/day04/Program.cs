using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = PuzzleInput.Take(1).SelectMany(line => line.Split(',')).Select(int.Parse).ToArray();

            List<BingoBoard> boards = ParseBoards(PuzzleInput.Skip(2)).ToList();

            int cnt = 0;

            bool weHaveAWinner = false;
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    board.Play(number);
                    if(board.HasBingo)
                    {
                        Console.WriteLine($"{++cnt}. {board.ScoreFactor * number}");
                        weHaveAWinner = true;
                    }
                }

                if(weHaveAWinner)
                {
                    break;
                }
            }

            Console.WriteLine("day04 completed.");
        }

        private static IEnumerable<BingoBoard> ParseBoards(IEnumerable<string> inputLines)
        {
            int[][] numbers = new int[5][];
            int rowNum = 0;

            foreach (var line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                int[] row = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                numbers[rowNum++] = row;

                if(rowNum == 5)
                {
                    yield return new BingoBoard(numbers);
                    numbers = new int[5][];
                    rowNum = 0;
                }    
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day04.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day04.Input.PuzzleInput.txt");
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
