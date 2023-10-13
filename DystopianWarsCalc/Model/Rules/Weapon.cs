using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Rules
{
    public class Weapon
    {
        internal Dictionary<WeaponRange, WeaponActionDice> actionDice;
        public IReadOnlyDictionary<WeaponRange, WeaponActionDice> ActionDice
        {
            get
            {
                return actionDice;
            }
        }

        internal Dictionary<ModelStatus, List<WeaponQuality>> qualities;
        public IReadOnlyList<WeaponQuality> Qualities(ModelStatus status)
        {
            return qualities[status];
        }

        public IReadOnlyList<WeaponQuality> BattleReadyQualities
        {
            get
            {
                return qualities[ModelStatus.Battle_Ready];
            }
        }

        public IReadOnlyList<WeaponQuality> CrippledQualities
        {
            get
            {
                return qualities[ModelStatus.Crippled];
            }
        }

        public Weapon()
        {
            actionDice = new Dictionary<WeaponRange, WeaponActionDice>();
            actionDice.Add(WeaponRange.Point_Blank, new WeaponActionDice());
            actionDice.Add(WeaponRange.Closing, new WeaponActionDice());
            actionDice.Add(WeaponRange.Long, new WeaponActionDice());

            qualities = new Dictionary<ModelStatus, List<WeaponQuality>>();
        }

        public string Name { get; internal set; } = string.Empty;

        public int? PointBlankLeadDice
        {
            get
            {
                return ActionDice[WeaponRange.Point_Blank].BattleReadyLeadDice;
            }
        }

        public int? PointBlankSupportDice
        {
            get
            {
                return ActionDice[WeaponRange.Point_Blank].BattleReadySupportDice;
            }
        }

        public int? ClosingLeadDice
        {
            get
            {
                return ActionDice[WeaponRange.Closing].BattleReadyLeadDice;
            }
        }

        public int? ClosingSupportDice
        {
            get
            {
                return ActionDice[WeaponRange.Closing].BattleReadySupportDice;
            }
        }

        public int? LongLeadDice
        {
            get
            {
                return ActionDice[WeaponRange.Long].BattleReadyLeadDice;
            }
        }

        public int? LongSupportDice
        {
            get
            {
                return ActionDice[WeaponRange.Long].BattleReadySupportDice;
            }
        }

        public bool IsDisabledWhenCrippled
        {
            get
            {
                return ActionDice[WeaponRange.Point_Blank].CrippledLeadDice.GetValueOrDefault() == Defines.WeaponActionDiceEmptyDefaultValue
                    && ActionDice[WeaponRange.Closing].CrippledLeadDice.GetValueOrDefault() == Defines.WeaponActionDiceEmptyDefaultValue
                    && ActionDice[WeaponRange.Long].CrippledLeadDice.GetValueOrDefault() == Defines.WeaponActionDiceEmptyDefaultValue
                    && !(ActionDice[WeaponRange.Point_Blank].BattleReadyLeadDice.GetValueOrDefault() == Defines.WeaponActionDiceEmptyDefaultValue
                    && ActionDice[WeaponRange.Closing].BattleReadyLeadDice.GetValueOrDefault() == Defines.WeaponActionDiceEmptyDefaultValue
                    && ActionDice[WeaponRange.Long].BattleReadyLeadDice.GetValueOrDefault() == Defines.WeaponActionDiceEmptyDefaultValue
                    );
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
