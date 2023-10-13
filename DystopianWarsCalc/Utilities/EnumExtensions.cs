using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Utilities
{
    public static class Enums
    {
        // https://stackoverflow.com/questions/642542/how-to-get-next-or-previous-enum-value-in-c-sharp

        public static T Next<T>(this T v) where T : struct
        {
            return System.Enum.GetValues(v.GetType())
                .Cast<T>()
                .Concat(new[] { default(T) })
                .SkipWhile(e => !v.Equals(e))
                .Skip(1)
                .First();
        }

        public static T Previous<T>(this T v) where T : struct
        {
            return System.Enum.GetValues(v.GetType())
                .Cast<T>()
                .Concat(new[] { default(T) })
                .Reverse()
                .SkipWhile(e => !v.Equals(e))
                .Skip(1)
                .First();
        }
    }
}
