using System;
using System.Windows.Input;

namespace TravelAgency.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object>? _canExecute;

        public RelayCommand(Action<object> executeAction) : this(executeAction, null)
        {
            _execute = executeAction;
        }

        public RelayCommand(Action<object> executeAction, Predicate<object>? canExecute)
        {
            _execute = executeAction;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return parameter != null && (_canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(object? parameter)
        {
            if (parameter != null) _execute(parameter);
        }
    }
}
