using DWListBuilder.Utilities;
using DystopianWarsCalc.Model.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWListBuilder.ViewModel.Components
{
    public class SpecialRuleEditiorVM : PropertyChangedBase
    {
        public SpecialRuleEditiorVM(SpecialRule specialRule)
        {
            SpecialRule = specialRule;
        }

        public SpecialRule SpecialRule { get; private set; }
    }
}
