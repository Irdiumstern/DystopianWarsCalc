using DystopianWarsCalc.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Rules
{
    public class WeaponQuality
    {
        public CommonWeaponQuality? CommonQuality { get; internal set; }

        private string? name = null;
        public string? Name
        {
            get
            {
                return name ?? CommonQuality.ToString();
            }
            internal set
            {
                name = value;
            }
        }

        public WeaponQuality(CommonWeaponQuality? commonQuality, string name = null)
        {
            CommonQuality = commonQuality;
            Name = name;
        }

    }
}
