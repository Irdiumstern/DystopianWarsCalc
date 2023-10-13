using DystopianWarsCalc.Model.DiceRoller;
using DystopianWarsCalc.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Utilities
{
    internal static class ModelBuilder
    {
        public static IList<DWModel> GetTargets()
        {
            IList<DWModel> targets = new List<DWModel>();
            targets.Add(BuildFrigateToten());
            targets.Add(BuildCruiserBlucher());
            targets.Add(BuildBattleshipElector());
            return targets;
        }

        internal static DWModel BuildFrigateToten()
        {
            var model = new DWModel();
            model.PositionTrait = PositionTrait.Surface_Unit;
            model.Datasheet.BattleReadyProfile.Mass = 1;
            model.Armor = 5;
            model.Citadel = 12;
            model.Hull = 3;
            model.AerialDefence = 3 + 4;
            model.SubmergedDefence = 4;
            model.Name = "Toten Heavy Destroyer";
            return model;
        }

        internal static DWModel BuildCruiserBlucher()
        {
            var model = new DWModel();
            model.PositionTrait = PositionTrait.Surface_Unit;
            model.Datasheet.BattleReadyProfile.Mass = 2;
            model.Datasheet.CrippledProfile.Mass = 2;
            model.Armor = 6;
            model.ArmorCrippled = 6;
            model.Citadel = 12;
            model.CitadelCrippled = 11;
            model.Hull = 4;
            model.HullCrippled = 4;
            model.AerialDefence = 4;
            model.SubmergedDefence = 4;
            model.Name = "Blucher Cruiser";
            return model;
        }
        internal static DWModel BuildBattleshipElector()
        {
            var model = new DWModel();
            model.PositionTrait = PositionTrait.Surface_Unit;
            model.Datasheet.BattleReadyProfile.Mass = 3;
            model.Datasheet.CrippledProfile.Mass = 3;
            model.Armor = 8;
            model.ArmorCrippled = 8;
            model.Citadel = 16;
            model.CitadelCrippled = 15;
            model.Hull = 9;
            model.HullCrippled = 3;
            model.Name = "Elector Battleship";
            return model;
        }
    }
}
