using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal abstract class Rule
    {
        private List<List<string>> options;
        private Func<string, Rule> ruleLookup;

        protected Rule(List<List<string>> options, Func<string, Rule> ruleLookup)
        {
            this.options = options;
            this.ruleLookup = ruleLookup;
        }

        internal static KeyValuePair<string, Rule> FromString(string rule, Func<string, Rule> ruleLookup)
        {
            string[] ruleDef = rule.Split(':');
            var ruleId = ruleDef[0];
            string[] optionsDef = ruleDef[1].Split('|');
            var options = new List<List<string>>(optionsDef.Length);
            foreach (var optionDef in optionsDef)
            {
                string[] sequenceDef = optionDef.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<string> sequence = new List<string>(sequenceDef.Length);
                sequence.AddRange(sequenceDef.Select(item => item.Trim('"')));
                options.Add(sequence);
            }

            IEnumerable<string> terminals = options.SelectMany(options => options);
            if (terminals.Count() == 1 && terminals.Where(t => char.IsLetter(t[0])).Any())
            {
                return new KeyValuePair<string, Rule>(ruleId, new Literal(terminals.First()));
            }
            else if (options.Count == 1)
            {
                if(options.First().Take(2).Count() == 1)
                {
                    return new KeyValuePair<string, Rule>(ruleId, new Reference(options.First().First(), ruleLookup));    
                }
                else
                {
                    return new KeyValuePair<string, Rule>(ruleId, new Sequence(options.First(), ruleLookup));
                }
            }
            else if (options.Count == 2)
            {
                return new KeyValuePair<string, Rule>(ruleId, new Options(options.Select(seq => seq.Count == 1 ? (Rule)new Reference(seq.First(), ruleLookup) : (Rule)new Sequence(seq, ruleLookup))));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal abstract IEnumerable<string> MatchMessage(string message);
    }
}