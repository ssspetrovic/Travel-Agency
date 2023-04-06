using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TravelAgency.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Action _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        public RelayCommand(Action executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null!;
        }

        public RelayCommand(Action executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return parameter != null && _canExecuteAction(parameter);
        }

        public void Execute(object? parameter)
        {
            if (parameter != null) _executeAction();
        }
    }
}
