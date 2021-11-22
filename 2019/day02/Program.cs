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
            var program = PuzzleInput
                .SelectMany(l => l.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(s => int.Parse(s))
                .ToArray();

            program[1] = 12;
            program[2] = 2;

            int pc = 0;
            bool halt = false;

            while (!halt)
            {
                int opCode = program[pc];
                int aPointer = 0;
                int bPointer = 0;
                int cPointer = 0;
                
                 switch (opCode)
                 {
                    case 1:
                        aPointer = program[pc + 1];
                        bPointer = program[pc + 2];
                        cPointer = program[pc + 3];
                        program[cPointer] = program[aPointer] + program[bPointer];
                        break;
                    case 2:
                        aPointer = program[pc + 1];
                        bPointer = program[pc + 2];
                        cPointer = program[pc + 3];
                        program[cPointer] = program[aPointer] * program[bPointer];
                        break;
                    case 99:
                        halt = true;
                        break;
                    default:
                        Console.WriteLine("Unknown opcode. Exiting.");
                        halt = true;
                        break;
                 }
                pc += 4;
            }

            Console.WriteLine(program[0]);

            Console.WriteLine("day02 completed.");
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
