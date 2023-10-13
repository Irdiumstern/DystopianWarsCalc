using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.DiceRoller
{
    public class Attack
    {
        public IList<int> DicePools { get; set; } = new List<int>();
        public int ContributingWeapons { get; set; }
        public bool Fusilade { get; set; }
        public bool Sustained { get; set; }
        public bool Devastating { get; set; }
        public bool RerollBlanks { get; set; }
        public bool Hazardous { get; set; }
        public bool Aerial { get; set; }
        public bool Submerged { get; set; }
        public bool Gunnery { get; set; }
        public bool Broadside { get; set; }
        public bool Rail { get; set; }
        public bool Arc { get; set; }
        public bool Voltaic { get; set; }
        public bool Bomb { get; set; }
        public bool HighVelocity { get; set; }
        public bool Homing { get; internal set; }
        public bool Torpedo { get; internal set; }
        public bool Piercing { get; internal set; }
        public bool AllowIntercept { get; set; }
        public int ShotsPerSRS { get; set; }
        public bool Magnetic { get; set; }
        public bool Indirect { get; set; }
    }
}
