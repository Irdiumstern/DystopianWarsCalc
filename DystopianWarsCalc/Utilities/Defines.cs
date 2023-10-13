using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Utilities
{
    public class Defines
    {
        public const string ORBATPath = @"Resources\ORBATs";
        public const string ORBATStorePath = @"Data\ORBATS";
        public const int DefaultEndingBlockIndex = -1;
        public const int InvalidAmount = -1;
        public static readonly int? WeaponActionDiceEmptyDefaultValue = 0;
        public const int AgitationSupportDice = 1;
        public const int NoCrippledMass = 1;

        public const int AgitationDefaultAmount = 1;
        public const string WeaponRefStartText = "These are the weapon";
        public const string WeaponLastTableHeader = "QUALITY";
        public const string WeaponEmpty = "-";
        public const string WeaponText = "WEAPON";
        public const string WeaponPointBlank = "POINT BLANK";
        public const string WeaponClosing = "CLOSING";
        public const string WeaponLong = "LONG";
        public const string WeaponDiceAgitation = "M";

        public const string BlankSpaceReplacement = "_";
        internal const string BulletPoint = "•";
        public static readonly IList<string> WeaponDividerTexts = new List<string>() { "-", "–", "-" };

        internal const string DatasheetUnitComposition = "Unit Composition";
        internal const string DatasheetTraits = "Traits";
        internal const string DatasheetSpecialRules = "Special Rules";
        internal const string DatasheetSquadron = "Squadron:";
        internal const string DatasheetWeapons = "Weapons";
        internal const string DatasheetOptions = "Options";
        internal const string DatasheetBattleReady = "Battle Ready";
        internal const string DatasheetCrippled = "Crippled";
        internal const string DatasheetMassMarker = "M";
        internal static IReadOnlyList<string> DatasheetKeywords = new List<string>() { DatasheetUnitComposition, DatasheetTraits, DatasheetSpecialRules, DatasheetSquadron, DatasheetWeapons, DatasheetOptions, DatasheetBattleReady, DatasheetCrippled };

        internal static readonly Dictionary<string, int> SquadronNumbers = new Dictionary<string, int>() { { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 } };
        internal const string SquadronTagAttached = "Attached";
        public const string SquadronCostTag = "pts";
    }
}
