using System;

namespace day25
{
    class Program
    {
        static void Main(string[] args)
        {
            long firstPublicKey = 8252394;
            long secondPublicKey = 6269621;
            int firstLoopSize = 0;
            int secondLoopSize = 0;
            long computedPublicKey = 1;

            for (int i = 0; i < Int32.MaxValue; i++)
            {
                int loopSize = i + 1;
                
                computedPublicKey *= 7;
                computedPublicKey %= 20201227;

                if (computedPublicKey == firstPublicKey)
                {
                    firstLoopSize = loopSize;
                    break;
                }
                if(computedPublicKey == secondPublicKey)
                {
                    secondLoopSize = loopSize;
                    break;
                }
            };

            Console.WriteLine("{0}, {1}", firstLoopSize, secondLoopSize);
            Console.WriteLine("{0}", (secondLoopSize > 0) ? Transform(firstPublicKey, secondLoopSize) : Transform(secondPublicKey, firstLoopSize));
        }

        private static long Transform(long subjectNumber, int loopSize)
        {
            long result = 1;
            for (int i = 0; i < loopSize; i++)
            {
                result *= subjectNumber;
                result %= 20201227;
            }

            return result;
        }
    }
}
