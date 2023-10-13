using DystopianWarsCalc.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.DiceRoller
{
    public class DicePoolResult
    {
        public IDictionary<DiceResult, int> Result { get; set; }

        public int Hits { get; set; }

        public int Defences { get; set;}
        public double FinalHits { get; set; }
        public int Rerolls { get; set; }

        public int InitialDicePool { get; set; }

        public int BaseDamageDone { get; set; }
        public bool CitadelBreached { get; set; }
        public bool CatastrophicExplosion { get; set; }
        public int TotalDamageDone { get { return Math.Clamp(BaseDamageDone + (this.CatastrophicExplosion ? 2 : 0), 0,MaxDamage); } }
        public int MaxDamage { get; set; }
        public int DisorderDone { get; set; }
        public int CriticalDamageDone { get; set; }
    }
}
