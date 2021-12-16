using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day16
{
    class Program
    {
        private static Dictionary<char, byte> mapping = new Dictionary<char, byte>()
        {
            {'0', 0b0000},
            {'1', 0b0001},
            {'2', 0b0010},
            {'3', 0b0011},
            {'4', 0b0100},
            {'5', 0b0101},
            {'6', 0b0110},
            {'7', 0b0111},
            {'8', 0b1000},
            {'9', 0b1001},
            {'A', 0b1010},
            {'B', 0b1011},
            {'C', 0b1100},
            {'D', 0b1101},
            {'E', 0b1110},
            {'F', 0b1111}
        };

        static void Main(string[] args)
        {
            var message = PuzzleInput.Select(line => ConvertToBinary(line)).SelectMany(p => Packet.FromBitStream(new BitStream(p))).First();

            Console.WriteLine(message.SumVersions());

            Console.WriteLine("day16 completed.");
        }

        private static byte[] ConvertToBinary(string line)
        {
            var result = new byte[(line.Length / 2)];
            
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(mapping[line[2 * i]] << 4 | mapping[line[2 * i + 1]]);
            }
            
            return result;
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day16.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day16.Input.PuzzleInput.txt");
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
