using System;
using System.Text.RegularExpressions;

namespace day19.Model
{
    internal class LiteralRuleBuilder : RuleBuilder
    {
        readonly Regex template = new Regex(@"^(\d+):\s""(\w)""$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Match m;

        internal override Rule Build()
        {
            return new LiteralRule(
                Int32.Parse(m.Groups[1].Value), 
                m.Groups[2].Value[0]);
        }

        internal override bool ReadDefinition(string ruleDef)
        {
            m = template.Match(ruleDef);
            return m.Success;
        }

        internal override bool ResolveReferences(Rule[] rules)
        {
            return true;
        }
    }
}