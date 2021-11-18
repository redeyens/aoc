using System.Collections.Generic;
using System.Linq;

namespace day19.Model
{
    internal class RulesRepository
    {
        private readonly IEnumerable<RuleBuilder> ruleBuilders = new RuleBuilder[] {
            new LiteralRuleBuilder(),
            new RepeatingRuleBuilder(),
            new SequenceRuleBuilder(),
            new NestingRuleBuilder(),
            new OptionRuleBuilder(),
        };
        Rule[] rules;

        public RulesRepository(IEnumerable<string> ruleDefinitions)
        {
            var parsingQ = new Queue<string>(ruleDefinitions);
            rules = new Rule[1000/*parsingQ.Count*/];

            while(parsingQ.Count > 0)
            {
                var ruleDef = parsingQ.Dequeue();
                
                foreach(var builder in ruleBuilders)
                {
                    if(builder.ReadDefinition(ruleDef))
                    {
                        if(builder.ResolveReferences(rules))
                        {
                            Rule rule = builder.Build();
                            rules[rule.Id] = rule;
                            break;
                        }
                        
                        parsingQ.Enqueue(ruleDef);
                    }
                }
            }
        }

        public Rule MasterRule => rules[0];

        public override string ToString()
        {
            return string.Join("\n", rules.Where(r => r != null));
        }
    }
}