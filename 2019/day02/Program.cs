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
            var originalProgram = PuzzleInput
                .SelectMany(l => l.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(s => int.Parse(s))
                .ToArray();

            bool found = false;

            for (int noun = 0; noun < 100 && !found; noun++)
            {
                for (int verb = 0; verb < 100 && !found; verb++)
                {
                    int[] program = ExecuteProgram(originalProgram, noun, verb);

                    if(program[0] == 19690720)    
                    {
                        Console.WriteLine(100 * noun + verb);
                        found = true;
                    }    
                }
            }

            Console.WriteLine("day02 completed.");
        }

        private static int[] ExecuteProgram(int[] originalProgram, int noun, int verb)
        {
            var program = new int[originalProgram.Length];
            Array.Copy(originalProgram, program, originalProgram.Length);
            program[1] = noun;
            program[2] = verb;

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

            return program;
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
