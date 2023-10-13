using DystopianWarsCalc.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Rules
{
    public class Profile
    {
        public int Mass { get; set; } = Defines.InvalidAmount;

        public int Speed { get; set; } = Defines.InvalidAmount;

        public int TurnLimit { get; set; } = Defines.InvalidAmount;

        public int Armor { get; set; } = Defines.InvalidAmount;

        public int Citadel { get; set; } = Defines.InvalidAmount;

        public int AerialDefence { get; set; } = Defines.InvalidAmount;

        public int SubmergedDefence { get; set; } = Defines.InvalidAmount;

        public int Fray { get; set; } = Defines.InvalidAmount;

        public int Hull { get; set; } = Defines.InvalidAmount;

        public bool HasErrors
        {
            get
            {
                return Mass == Defines.InvalidAmount || Speed == Defines.InvalidAmount || TurnLimit == Defines.InvalidAmount
                    || Armor == Defines.InvalidAmount || Citadel == Defines.InvalidAmount || SubmergedDefence == Defines.InvalidAmount
                    || Fray == Defines.InvalidAmount || Hull == Defines.InvalidAmount; 
            }
        }
    }
}
