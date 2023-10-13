using DWListBuilder.Utilities;
using DWListBuilder.ViewModel.Components;
using DystopianWarsCalc.Model.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWListBuilder.ViewModel
{
    internal class OrbatEditorVM : PropertyChangedBase
    {
        private List<Orbat> orbats;

        public IReadOnlyList<Orbat> Orbats
        {
            get
            {
                return this.orbats;
            }
        }

        public OrbatEditorVM(List<Orbat> orbats)
        {
            this.orbats = orbats;
        }

        private Orbat selectedOrbat;

        public Orbat SelectedOrbat
        {
            get { return selectedOrbat; }
            set
            {
                if (selectedOrbat != value)
                {
                    selectedOrbat = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.Datasheets));
                    this.OnPropertyChanged(nameof(this.SpecialRules));
                }
            }
        }

        public IReadOnlyList<DatasheetEditorVM>? Datasheets
        {
            get
            {
                return this.selectedOrbat?.Datasheets.Select(x => new DatasheetEditorVM(x)).ToList();
            }
        }

        public IReadOnlyList<SpecialRuleEditiorVM> SpecialRules
        {
            get
            {
                return this.selectedOrbat?.SpecialRules.Select(x => new SpecialRuleEditiorVM(x)).ToList();
            }
        }

    }
}
