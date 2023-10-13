using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Model.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.DiceRoller
{
    public class DWModel
    {
        public DWModel(Datasheet datasheet)
        {
            Datasheet = datasheet;
        }

        public DWModel(int armor, int citadel, int aerialDefence, int submergedDefence, int hull, bool ignoreCatastrophic, bool ablative, int shield, bool obscured, int points, PositionTrait positionTrait)
        {
            this.Datasheet = new Datasheet();
            this.Armor = armor;
            this.Citadel = citadel;
            this.AerialDefence = aerialDefence;
            this.SubmergedDefence = submergedDefence;
            this.Hull = hull;
            IgnoreCatastrophic = ignoreCatastrophic;
            Ablative = ablative;
            Shield = shield;
            Obscured = obscured;
            Points = points;
            PositionTrait = positionTrait;
            
        }

        public DWModel()
        {
            this.Datasheet = new Datasheet();
        }

        public Datasheet Datasheet { get; private set; }

        public int Armor { get { return this.Datasheet.BattleReadyProfile.Armor; } set { this.Datasheet.BattleReadyProfile.Armor = value; } }
        public int ArmorCrippled { get { return this.Datasheet.CrippledProfile?.Armor ?? 0; } set { if (this.Datasheet.CrippledProfile != null) { this.Datasheet.CrippledProfile.Armor = value; } } }
        public int Citadel { get { return this.Datasheet.BattleReadyProfile.Citadel; } set { this.Datasheet.BattleReadyProfile.Citadel = value; } }
        public int CitadelCrippled { get { return this.Datasheet.CrippledProfile?.Citadel ?? 0; } set { if (this.Datasheet.CrippledProfile != null) { this.Datasheet.CrippledProfile.Citadel = value; } } }
        public int AerialDefence { get { return this.Datasheet.BattleReadyProfile.AerialDefence; } set { this.Datasheet.BattleReadyProfile.AerialDefence = value; } }
        public int AerialDefenceCrippled { get { return this.Datasheet.CrippledProfile?.AerialDefence ?? 0; } set { if (this.Datasheet.CrippledProfile != null) { this.Datasheet.CrippledProfile.AerialDefence = value; } } }
        public int SubmergedDefence { get { return this.Datasheet.BattleReadyProfile.SubmergedDefence; } set { this.Datasheet.BattleReadyProfile.SubmergedDefence = value; } }
        public int SubmergedDefenceCrippled { get { return this.Datasheet.CrippledProfile?.SubmergedDefence ?? 0; } set { if (this.Datasheet.CrippledProfile != null) { this.Datasheet.CrippledProfile.SubmergedDefence = value; } } }
        public int Hull { get { return this.Datasheet.BattleReadyProfile.Hull; } set { this.Datasheet.BattleReadyProfile.Hull = value; } }
        public int HullCrippled { get { return this.Datasheet.CrippledProfile?.Hull ?? 0; } set { if (this.Datasheet.CrippledProfile != null) { this.Datasheet.CrippledProfile.Hull = value; } } }
        public int Points { get; set; }
        public bool IgnoreCatastrophic { get; set; }
        public bool Ablative { get; set; }
        public bool ShieldGenerator { get; set; }
        public int Shield { get; set; }
        public bool Obscured { get; set; }
        public bool IsMass1 { get { return this.Datasheet.BattleReadyProfile.Mass == 1; } }
        public PositionTrait PositionTrait { get; set; }
        public string Name { get; set; }
        public bool LuminiferousDefences { get; set; }
    }
}
