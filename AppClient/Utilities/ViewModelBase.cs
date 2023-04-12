using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Utilities
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public virtual string NameViewModel { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Notify([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
