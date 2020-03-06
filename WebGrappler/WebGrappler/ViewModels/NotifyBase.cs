using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGrappler.ViewModels
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string property)
        {
            if(PropertyChanged!=null)
            PropertyChanged(this,new PropertyChangedEventArgs(property));
        }
    }
}
