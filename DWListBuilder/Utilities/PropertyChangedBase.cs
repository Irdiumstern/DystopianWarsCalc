using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DWListBuilder.Utilities
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private bool isEditing = false;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                if (this.isEditing != value)
                {
                    isEditing = value;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged(nameof(this.IsReadOnly));
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return !this.isEditing;
            }
        }
    }
}
