using DWListBuilder.Utilities;
using DystopianWarsCalc.Model.Enum;
using DystopianWarsCalc.Model.Rules;
using DystopianWarsCalc.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DWListBuilder.View.Converters
{
    public class WeaponActionDiceConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var weapon = value as Weapon;
            var tokens = (parameter as string)?.Split(UIDefines.ParameterSplitValue);
            if (weapon != null && tokens != null && tokens.Count() == 2)
            {
                var weaponRange = (WeaponRange)Enum.Parse(typeof(WeaponRange), tokens[0]);
                var modelStatus = (ModelStatus)Enum.Parse(typeof(ModelStatus), tokens[1]);
                var leadDice = weapon.ActionDice[weaponRange].ActionDice[new Tuple<ModelStatus, WeaponType>(modelStatus, WeaponType.Lead)];
                var supportDice = weapon.ActionDice[weaponRange].ActionDice[new Tuple<ModelStatus, WeaponType>(modelStatus, WeaponType.Support)];
                if (leadDice == Defines.WeaponActionDiceEmptyDefaultValue)
                {
                    return Defines.WeaponEmpty;
                }
                else if (weapon.Qualities(modelStatus).Any(x => x.CommonQuality == CommonWeaponQuality.Agitation))
                {
                    return leadDice.ToString() + "xM";
                }
                else
                {
                    StringBuilder result = new StringBuilder();
                    result.Append(leadDice.ToString());
                    result.Append(" (");
                    result.Append(supportDice.GetValueOrDefault() == Defines.WeaponActionDiceEmptyDefaultValue ? Defines.WeaponEmpty : supportDice.ToString());
                    result.Append(")");

                    return result.ToString();
                }
            }
            else
            {
                return string.Empty;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
