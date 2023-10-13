using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Model.DiceRoller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Logic
{
    public class DiceRollCalculator
    {
        public void Calc (IList<Attacker> attackers, IList<DWModel> targets)
        {
            foreach (Attacker attacker in attackers)
            {
                foreach (DWModel target in targets)
                {
                    var run = new TestRun(attacker, target);

                }
            }
        }

    }
}
