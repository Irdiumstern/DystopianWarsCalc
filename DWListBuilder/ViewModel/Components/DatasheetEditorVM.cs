using DWListBuilder.Utilities;
using DystopianWarsCalc.Model.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWListBuilder.ViewModel.Components
{
    public class DatasheetEditorVM : PropertyChangedBase
    {
        public DatasheetEditorVM(Datasheet datasheet)
        {
            Datasheet = datasheet;
        }

        public Datasheet Datasheet { get; private set; }
    }
}
