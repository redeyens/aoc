using System;
using System.Collections.Generic;
using System.Linq;

namespace day19_at2
{
    internal abstract class Rule
    {
        internal static Rule FromString(string ruleDefinition, Func<string, Rule> ruleLookup)
        {
            if(ruleDefinition.Contains('|'))
            {
                return ParseOptions(ruleDefinition, ruleLookup);
            }

            if(ruleDefinition.Contains('"'))
            {
                return ParseLiteral(ruleDefinition);
            }

            return ParseSequence(ruleDefinition, ruleLookup);
        }

        private static Rule ParseLiteral(string ruleDefinition)
        {
            return new Literal(ruleDefinition.Trim().Trim('"'));
        }

        private static Rule ParseOptions(string optionsDefinition, Func<string, Rule> ruleLookup)
        {
            return new Options(optionsDefinition.Split('|').Select(optDef => ParseSequence(optDef, ruleLookup)));
        }

        private static Rule ParseSequence(string sequenceDefinition, Func<string, Rule> ruleLookup)
        {
            var refs = sequenceDefinition
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(r => new Reference(r, ruleLookup));
            if(refs.Take(2).Count() == 1)
            {
                return refs.First();    
            }
            else
            {
                return new Sequence(refs);
            }
        }

        internal abstract IEnumerable<string> MatchMessage(string message);
    }
}