using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Rules
{
    public class SpecialRule
    {
        public SpecialRule(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
