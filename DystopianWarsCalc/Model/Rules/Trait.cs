using DystopianWarsCalc.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Rules
{
    public class Trait
    {
        public Trait(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public string? Description { get; set; }    

        public PositionTrait? PositionTrait { get; set; }

        public bool IsPositionTrait { get { return this.PositionTrait.HasValue; } }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
