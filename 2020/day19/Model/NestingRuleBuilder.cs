using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day19.Model
{
    internal class NestingRuleBuilder : RuleBuilder
    {
       readonly Regex template = new Regex(@"^(\d+):(\s\d+)(\s\d+) \|\2 \1\3$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Match m;
        private Rule reference1;
        private Rule reference2;

        internal override Rule Build()
        {
            return new NestingRule(Int32.Parse(m.Groups[1].Value), reference1, reference2);
        }

        internal override bool ReadDefinition(string ruleDef)
        {
            m = template.Match(ruleDef);
            return m.Success;
        }

        internal override bool ResolveReferences(Rule[] rules)
        {
            var missingReferences = m.Groups[2].Captures
                .Concat(m.Groups[3].Captures)
                .Select(c => Int32.Parse(c.Value))
                .Where(rid => rules[rid] == null);
            if(missingReferences.Any())
            {
                return false;
            }
            
            reference1 = m.Groups[2].Captures
                .Select(c => Int32.Parse(c.Value))
                .Select(rid => rules[rid])
                .First();
            reference2 = m.Groups[3].Captures
                .Select(c => Int32.Parse(c.Value))
                .Select(rid => rules[rid])
                .First();

            return true;
        }
    }
}