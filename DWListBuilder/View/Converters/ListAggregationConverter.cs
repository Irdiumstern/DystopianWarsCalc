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
    public class ListAggregationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var weaponQualities = value as IList<WeaponQuality>;
            if (weaponQualities != null)
            {
                if (weaponQualities.Count == 0)
                {
                    return Defines.WeaponEmpty;
                }
                else
                {
                    return weaponQualities.Select(x => x.Name?.Replace(Defines.BlankSpaceReplacement, " ") ?? String.Empty).Aggregate((x, y) => x + ", " + y);
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
