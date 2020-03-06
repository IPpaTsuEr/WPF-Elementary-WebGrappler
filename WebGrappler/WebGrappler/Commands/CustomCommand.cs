using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebGrappler.Commands
{
    public class CustomCommand : ICommand
    {
        public CustomCommand() { }
        public CustomCommand(Func<object, bool> judger ,Action<object> excuter) {
            func = judger;
            Act = excuter;
        }
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        readonly Action<object> Act;
        readonly Func<object , bool> func;

        public bool CanExecute(object parameter)
        {
            if (func == null || parameter==null) return false;
            return func(parameter);
        }

        public void Execute(object parameter)
        {
            if (Act != null && parameter!=null)
            {
                Act(parameter);
            }
        }
    }
}
