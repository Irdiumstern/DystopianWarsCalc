using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.DiceRoller
{
    public class Attacker
    {
        public Attacker()
        {
            this.Attacks = new List<Attack>();
        }

        public Attacker(List<int> dicePools, bool fusilade = false, bool sustained = false, bool devastating = false, bool rail = false, bool gunnery = false)
        {
            this.Attacks = new List<Attack>();
            var a = new Attack();
            a.DicePools = dicePools;
            a.Fusilade = fusilade;
            a.Sustained = sustained;
            a.Devastating = devastating;
            a.Rail = rail;
            a.Gunnery = gunnery;
            this.Attacks.Add(a);
        }

        public IList<Attack> Attacks { get; private set; }
        public int PointCost { get; set; }
        public string Name { get; set; }
    }
}
