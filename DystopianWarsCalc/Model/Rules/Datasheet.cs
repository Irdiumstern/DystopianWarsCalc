using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace DystopianWarsCalc.Model.Rules
{
    public class Datasheet
    {
        public Datasheet()
        {
            this.profiles.Add(ModelStatus.Battle_Ready, new Profile());
            this.profiles.Add(ModelStatus.Crippled, new Profile());
        }

        internal Dictionary<ModelStatus, Profile> profiles = new Dictionary<ModelStatus, Profile>();

        public IReadOnlyDictionary<ModelStatus, Profile> Profiles { get { return this.profiles; } }

        public Profile BattleReadyProfile
        {
            get
            {
                return this.profiles[ModelStatus.Battle_Ready];
            }
        }

        public Profile? CrippledProfile
        {
            get
            {
                if (this.profiles[ModelStatus.Battle_Ready].Mass > 1)
                {
                    return this.profiles[ModelStatus.Crippled];
                }
                else
                {
                    return null;
                }
            }
        }

        public IReadOnlyDictionary<ModelStatus, Profile> DisplayProfiles
        {
            get
            {
                if (this.Profiles[ModelStatus.Battle_Ready].Mass == Defines.NoCrippledMass)
                {
                    return new Dictionary<ModelStatus, Profile> { { ModelStatus.Battle_Ready, this.Profiles[ModelStatus.Battle_Ready] } };
                }
                else
                {
                    return this.Profiles;
                }
            }
        }

        internal List<Tuple<Weapon, FireArc>> weapons = new List<Tuple<Weapon, FireArc>>();
        public IReadOnlyList<Tuple<Weapon, FireArc>> Weapons
        {
            get
            {
                return this.weapons;
            }
        }

        public string Name { get; set; }

        public int MinAmount { get; set; } = Defines.InvalidAmount;

        public int MaxAmount { get; set; } = Defines.InvalidAmount;

        public int BasePointCost { get; set; } = Defines.InvalidAmount;

        public int PointCostPerModel { get; set; } = Defines.InvalidAmount;

        public bool CanAttach { get; set; }

        internal List<Trait> traits = new List<Trait>();

        public IReadOnlyList<Trait> Traits { get { return this.traits; } }

        internal List<SpecialRule> specialRules = new List<SpecialRule>();


        public IReadOnlyList<SpecialRule> SpecialRules { get { return this.specialRules; } }

        public bool HasErrors
        {
            get
            {
                return this.Traits.Count == 0 || this.SpecialRules.Count == 0 || string.IsNullOrWhiteSpace(this.Name);
            }
        }

        public bool HasProfileErrors
        {
            get
            {
                return this.Profiles[ModelStatus.Battle_Ready].HasErrors || (this.Profiles[ModelStatus.Crippled].HasErrors && this.Profiles[ModelStatus.Battle_Ready].Mass != Defines.NoCrippledMass);
            }
        }

        public override string ToString()
        {
            return this.Name + (this.HasErrors ? " ERROR" : String.Empty);
        }

        #region Internal Processing
        internal int startingBlockIndex;

        internal int endingBlockIndex;

        internal TextBlock traitBlockForSpecialRules;

        #endregion Internal Processing
    }
}
