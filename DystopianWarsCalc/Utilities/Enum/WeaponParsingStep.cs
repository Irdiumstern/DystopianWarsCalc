using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Utilities.Enum
{
    internal enum WeaponParsingStep
    {
        None = 0,
        Name,
        //BRPointBlankLead,
        //BRPointBlankSupport,
        //BRClosingLead,
        //BRClosingSupport,
        //BRLongLead,
        //BRLongSupport,
        BRDice,
        BRQualities,
        //CrippledPointBlankLead,
        //CrippledPointBlankSupport,
        //CrippledClosingLead,
        //CrippledClosingSupport,
        //CrippledLongLead,
        //CrippledLongSupport,
        CrippledDice,
        CrippledQualities,
    }
}
