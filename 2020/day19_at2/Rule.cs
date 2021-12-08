using System.Collections.Generic;

namespace day19_at2
{
    internal class Rule
    {
        private List<List<string>> options;

        public Rule(List<List<string>> options)
        {
            this.options = options;
        }

        public List<List<string>> Options => options;
    }
}