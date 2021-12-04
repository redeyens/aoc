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
            string[] inputBlocks = PuzzleInputText.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<int> numbersDrawn = inputBlocks[0].Split(',').Select(int.Parse);

            HashSet<BingoBoard> boards = new HashSet<BingoBoard>();
            foreach (BingoBoard board in inputBlocks[1..].Select(BingoBoard.FromString))
            {
                boards.Add(board);
            }

            bool weHaveAWinner = false;
            List<BingoBoard> workingSet = new List<BingoBoard>(boards.Count());
            foreach (int number in numbersDrawn)
            {
                workingSet.Clear();
                workingSet.AddRange(boards);
                foreach (BingoBoard board in workingSet)
                {
                    board.Play(number);
                    if(board.HasBingo)
                    {
                        if(boards.Count() == 1)
                        {
                            Console.WriteLine(board.ScoreFactor * number);

                            weHaveAWinner = true;
                        }
                       boards.Remove(board);
                    }
                }

                if(weHaveAWinner)
                {
                    break;
                }
            }

            Console.WriteLine("day04 completed.");
        }

        private static string TestInputText
        {
            get
            {
                return GetTextFromResource("day04.Input.TestInput.txt");
            }
        }

        private static string PuzzleInputText
        {
            get
            {
                return GetTextFromResource("day04.Input.PuzzleInput.txt");
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
