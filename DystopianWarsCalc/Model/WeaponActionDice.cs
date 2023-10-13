using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model
{
    public class WeaponActionDice
    {
        private Dictionary<Tuple<ModelStatus, WeaponType>, int?> actionDice;

        public IReadOnlyDictionary<Tuple<ModelStatus, WeaponType>, int?> ActionDice
        {
            get
            {
                return this.actionDice;
            }
        }

        public WeaponActionDice()
        {
            this.actionDice = new Dictionary<Tuple<ModelStatus, WeaponType>, int?>();
            this.actionDice.Add(new Tuple<ModelStatus, WeaponType>(ModelStatus.Battle_Ready, WeaponType.Lead), Defines.WeaponActionDiceEmptyDefaultValue);
            this.actionDice.Add(new Tuple<ModelStatus, WeaponType>(ModelStatus.Battle_Ready, WeaponType.Support), Defines.WeaponActionDiceEmptyDefaultValue);
            this.actionDice.Add(new Tuple<ModelStatus, WeaponType>(ModelStatus.Crippled, WeaponType.Lead), Defines.WeaponActionDiceEmptyDefaultValue);
            this.actionDice.Add(new Tuple<ModelStatus, WeaponType>(ModelStatus.Crippled, WeaponType.Support), Defines.WeaponActionDiceEmptyDefaultValue);
        }
        
        internal void SetActionDice(ModelStatus status, WeaponType type, int? value)
        {
            this.actionDice[new Tuple<ModelStatus, WeaponType>(status, type)] = value;
        }

        public int? BattleReadyLeadDice
        {
            get
            {
                return this.ActionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Battle_Ready, WeaponType.Lead)];
            }

            internal set
            {
                this.actionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Battle_Ready, WeaponType.Lead)] = value;
            }
        }

        public int? BattleReadySupportDice
        {
            get
            {
                return this.ActionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Battle_Ready, WeaponType.Support)];
            }

            internal set
            {
                this.actionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Battle_Ready, WeaponType.Support)] = value;
            }
        }
        public int? CrippledLeadDice
        {
            get
            {
                return this.ActionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Crippled, WeaponType.Lead)];
            }

            internal set
            {
                this.actionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Crippled, WeaponType.Lead)] = value;
            }
        }

        public int? CrippledSupportDice
        {
            get
            {
                return this.ActionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Crippled, WeaponType.Support)];
            }

            internal set
            {
                this.actionDice[new Tuple<ModelStatus, WeaponType>(ModelStatus.Crippled, WeaponType.Support)] = value;
            }
        }
    }
}
