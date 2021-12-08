using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal abstract class Rule
    {
        internal static Rule FromString(string rule, Func<string, Rule> ruleLookup)
        {
            string[] optionsDef = rule.Split('|');
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
                return new Literal(terminals.First());
            }
            else if (options.Count == 1)
            {
                if(options.First().Take(2).Count() == 1)
                {
                    return new Reference(options.First().First(), ruleLookup);    
                }
                else
                {
                    return new Sequence(options.First().Select(r => new Reference( r, ruleLookup)));
                }
            }
            else if (options.Count == 2)
            {
                return new Options(options.Select(seq => seq.Count == 1 ? 
                                                        (Rule)new Reference(seq.First(), ruleLookup) : 
                                                        (Rule)new Sequence(seq.Select(r => new Reference(r, ruleLookup)))));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal abstract IEnumerable<string> MatchMessage(string message);
    }
}