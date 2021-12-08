using System;
using System.Collections.Generic;
using System.IO;

namespace day19_at2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputBlocks = PuzzleInputText.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var messageParser = Parser.FromGrammar(inputBlocks[0]);

            messageParser.Update($"8: 42 | 42 8{Environment.NewLine}11: 42 31 | 42 11 31");

            int matchingMsgs = 0;
            foreach (var message in inputBlocks[1].Split(Environment.NewLine))
            {
                if(messageParser.TryParse(message, "0"))
                {
                    matchingMsgs++;
                }
            }

            Console.WriteLine(matchingMsgs);

            Console.WriteLine("day19_at2 completed.");
        }

        private static string TestInputText
        {
            get
            {
                return GetTextFromResource("day19_at2.Input.TestInput.txt");
            }
        }

        private static string PuzzleInputText
        {
            get
            {
                return GetTextFromResource("day19_at2.Input.PuzzleInput.txt");
            }
        }

        private static IEnumerable<string> TestInput
        {
            get
            {
                return GetLinesFromResource("day19_at2.Input.TestInput.txt");
            }
        }

        private static IEnumerable<string> PuzzleInput
        {
            get
            {
                return GetLinesFromResource("day19_at2.Input.PuzzleInput.txt");
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
