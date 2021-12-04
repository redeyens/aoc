using System;
using System.Collections.Generic;
using System.Linq;

namespace day04
{
    internal class BingoBoard
    {
        private int[][] numbers;
        private byte[] numbersDrawn = new byte[5];

        public BingoBoard(int[][] numbers)
        {
            this.numbers = numbers;
        }

        public bool HasBingo 
        { 
            get
            {
                return HasAnyRow() || HasAnyCol();
            } 
        }

        private bool HasAnyCol()
        {
            return numbersDrawn.Where(n => n == 31).Any();
        }

        private bool HasAnyRow()
        {
            return numbersDrawn.Aggregate(31, (res, b) => res &= b) > 0;
        }

        public int ScoreFactor 
        { 
            get
            {
                return GetNumbers().Zip(GetWeights(), (n, w) => n * w).Sum();
            } 
        }

        private IEnumerable<int> GetNumbers()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    yield return numbers[i][j];
                }
            }
        }

        private IEnumerable<int> GetWeights()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    yield return (((numbersDrawn[i] >> j) & 1) == 0) ? 1 : 0;
                }
            }
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
    }
}