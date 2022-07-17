using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mj.Common
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propName));
        }
    }
}
