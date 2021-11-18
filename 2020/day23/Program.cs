using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace day23
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> startingCups = new List<int>(){3, 6, 8, 1, 9, 5, 7, 4, 2};
            List<int> cups = new List<int>(1_000_000);
            int currentCupIndex = 0;
            int maxCup = startingCups.Max();
            int[] buffer = new int[3];

            cups.AddRange(startingCups);
            int remaining = cups.Capacity - startingCups.Count;
            for (int i = 0; i < remaining; i++)
            {
                maxCup = maxCup + 1;
                cups.Add(maxCup);
            }

            for (int i = 0; i < 10_000_000; i++)
            {
                // Console.WriteLine("-- Move {0} --", i + 1);
                // Console.WriteLine(string.Join(" ", cups));
                // Console.WriteLine(string.Join(" ", cups.Select((num, i) => (i == currentCupIndex)?"|":" ")));
                int destinationCup = (cups[currentCupIndex] + maxCup - 2) % maxCup + 1;

                for (int j = 0; j < buffer.Length; j++)
                {
                    int cupToRemoveIndex = (currentCupIndex + 1) % cups.Count;
                    buffer[j] = cups[cupToRemoveIndex];
                    cups.RemoveAt(cupToRemoveIndex);
                    if(cupToRemoveIndex < currentCupIndex)
                    {
                        currentCupIndex = (currentCupIndex + cups.Count - 1) % cups.Count;
                    }
                }
                // Console.WriteLine(string.Join(" ", buffer));
                
                int destinationCupIndex = -1;
                while (destinationCupIndex < 0)
                {
                    destinationCupIndex = cups.IndexOf(destinationCup);
                    destinationCup = (destinationCup + maxCup - 2) % maxCup + 1;
                }
                // Console.WriteLine(cups[destinationCupIndex]);
                // Console.WriteLine();

                for (int j = buffer.Length - 1; j >= 0; j--)
                {
                    cups.Insert(destinationCupIndex + 1, buffer[j]);
                    if(destinationCupIndex + 1 <= currentCupIndex)
                    {
                        currentCupIndex = (currentCupIndex + 1) % cups.Count;
                    }
                }

                currentCupIndex = (currentCupIndex + 1) % cups.Count;
            }

            int outputStartIndex = cups.IndexOf(1);
            // var outputBldr = new StringBuilder(cups.Count);
            // for (int i = 1; i < cups.Count; i++)
            // {
            //     outputBldr.Append(cups[(outputStartIndex + i) % cups.Count]);
            // }

            // Console.WriteLine(outputBldr.ToString());

            long result = (long)cups[(outputStartIndex + 1) % cups.Count] * cups[(outputStartIndex + 2) % cups.Count];

            Console.WriteLine(result);
        }
    }
}
