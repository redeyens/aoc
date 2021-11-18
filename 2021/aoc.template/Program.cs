using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.template
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string line in Input1)
            {
                Console.WriteLine(line);
            }

            foreach (string line in Input2)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("aoc.template completed.");
        }

        private static IEnumerable<string> Input1
        {
            get
            {
                return GetLinesFromResource("aoc.template.Input.Input1.txt");
            }
        }

        private static IEnumerable<string> Input2
        {
            get
            {
                return GetLinesFromResource("aoc.template.Input.Input2.txt");
            }
        }

        private static IEnumerable<string> GetLinesFromResource(string name)
        {
            using (Stream in1Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                using (TextReader in1Reader = new StreamReader(in1Stream))
                {
                    yield return in1Reader.ReadLine();
                }
            }
        }
    }
}
