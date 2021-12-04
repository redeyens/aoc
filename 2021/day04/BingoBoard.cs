using System;
using System.Collections.Generic;
using System.Linq;

namespace day04
{
    internal class BingoBoard
    {
        private const byte EntireRowSelected = 31;
        private int[][] numbers;
        private byte[] numbersDrawn = new byte[5];

        internal static BingoBoard FromString(string input)
        {
            int[][] numbers = new int[5][];
            int rowNum = 0;

            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                numbers[rowNum++] = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }

            return new BingoBoard(numbers);
        }

        internal void Play(int number)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(numbers[i][j] == number)
                    {
                        numbersDrawn[i] |= (byte)(1 << j);
                        return;
                    }
                }
            }
        }

        public bool HasBingo => HasAnyCol() || HasAnyRow();

        public int ScoreFactor =>  GetNumbers().Zip(GetWeights(), (n, w) => n * w).Sum();

        private BingoBoard(int[][] numbers)
        {
            this.numbers = numbers;
        }

        private bool HasAnyRow() => numbersDrawn.Where(n => n == EntireRowSelected).Any();

        private bool HasAnyCol() => numbersDrawn.Aggregate(EntireRowSelected, (res, row) => res &= row) > 0;

        private IEnumerable<int> GetNumbers() => numbers.SelectMany(row => row);

        private IEnumerable<int> GetWeights() =>numbersDrawn.SelectMany(row => WeightsFromByte(row));

        private IEnumerable<int> WeightsFromByte(byte row)
        {
            for (int j = 0; j < 5; j++)
            {
                yield return (((row >> j) & 1) == 0) ? 1 : 0;
            }
        }
    }
}