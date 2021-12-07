using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day19_at2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputBlocks = PuzzleInputText.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string,List<List<string>>> grammar = new Dictionary<string, List<List<string>>>();

            foreach (var rule in inputBlocks[0].Split(Environment.NewLine))
            {
                string[] ruleDef = rule.Split(':');
                string ruleId = ruleDef[0];

                string[] optionsDef = ruleDef[1].Split('|');
                List<List<string>> options = new List<List<string>>(optionsDef.Length);

                grammar[ruleId] = options;

                foreach (var optionDef in optionsDef)
                {
                    string[] sequenceDef = optionDef.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    List<string> sequence = new List<string>(sequenceDef.Length);
                    sequence.AddRange(sequenceDef.Select(item => item.Trim('"')));
                    options.Add(sequence);
                }
            }

            grammar["8"] = new List<List<string>>()
                {
                    new List<string>() { "42"},
                    new List<string>() { "42", "8"}
                };
            grammar["11"] = new List<List<string>>()
                {
                    new List<string>() {"42", "31"},
                    new List<string>() {"42", "11", "31"},
                };

            int matchingMsgs = 0;
            foreach (var message in inputBlocks[1].Split(Environment.NewLine))
            {
                if(MatchMessage(grammar, "0", message).Where(residual => string.IsNullOrEmpty(residual)).Any())
                {
                    matchingMsgs++;
                }
            }

            Console.WriteLine(matchingMsgs);

            Console.WriteLine("day19_at2 completed.");
        }

        private static IEnumerable<string> MatchMessage(Dictionary<string, List<List<string>>> grammar, string startingRule, string message)
        {
            List<List<string>> ruleOptions = null;

            // if the grammar does not contain this rule then it must be literal
            if(!grammar.TryGetValue(startingRule, out ruleOptions))
            {
                return MatchLiteral(grammar, startingRule, message);
            }
            else
            {
                return MatchOptions(grammar, grammar[startingRule], message);
            }
        }

        private static IEnumerable<string> MatchLiteral(Dictionary<string, List<List<string>>> grammar, string literal, string message)
        {
            if(message.StartsWith(literal))
            {
                yield return message.Substring(literal.Length);
            }
        }
        private static IEnumerable<string> MatchOptions(Dictionary<string, List<List<string>>> grammar, List<List<string>> ruleOptions, string message)
        {
            if(ruleOptions.Count == 1)
            {
                return MatchSequence(grammar, ruleOptions[0], message);
            }
            else
            {
                return MatchSequence(grammar, ruleOptions[0], message).Concat(MatchOptions(grammar, new List<List<string>>(ruleOptions.Skip(1)), message));
            }
        }
        private static IEnumerable<string> MatchSequence(Dictionary<string, List<List<string>>> grammar, List<string> ruleSequence, string message)
        {
            if(ruleSequence.Count == 1)
            {
                return MatchMessage(grammar, ruleSequence[0], message);
            }
            else
            {
                return MatchMessage(grammar, ruleSequence[0], message).SelectMany(residual => MatchSequence(grammar, new List<string>(ruleSequence.Skip(1)), residual));
            }
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
