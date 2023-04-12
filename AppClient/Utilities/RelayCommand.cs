using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlineCinema.Utilities
{
    public class RelayCommand : ICommand
    {
        private Action<object> _action;
        private Func<object, bool> _func;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> action, Func<object, bool> func = null)
        {
            _action = action;
            _func = func;
        }

        public bool CanExecute(object? parameter)
        {
            return _func == null || _func(parameter);
        }

        public void Execute(object? parameter)
        {
            _action(parameter);
        }
    }
}
