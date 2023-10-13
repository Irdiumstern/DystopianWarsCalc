using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.DiceRoller
{
    public class SalvoResult
    {
        public SalvoResult()
        {
            this.Results = new List<DicePoolResult>();
        }

        public IList<DicePoolResult> Results { get; set; }

        public int ShotsTaken { get { return this.Results.Count; } }
        public double AverageHitsPerDice { get { return this.Results.Select(x => x.FinalHits / x.InitialDicePool).Sum() / this.ShotsTaken; } }
        public double AverageHits
        {
            get
            {
                return this.Results.Select(x => x.FinalHits).Sum() / this.ShotsTaken;
            }
        }

        public double AverageDamage
        {
            get
            {
                return this.Results.Select(x => x.TotalDamageDone).Sum() / this.ShotsTaken;
            }
        }

        public int TotalDamageDone { get { return Results.Select(x => x.TotalDamageDone).Sum(); } }

        public int TotalCritsDone { get { return Results.Select(x => x.CriticalDamageDone).Sum(); } }

        public int TotalDisorderDone { get { return Results.Select(x => x.DisorderDone).Sum(); } }
        public int TotalCitadelBreached { get { return Results.Where(x => x.CitadelBreached).Count(); } }
    }
}
