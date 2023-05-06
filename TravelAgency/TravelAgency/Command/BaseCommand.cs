using System;
using System.Windows.Input;

namespace TravelAgency.Command
{
    public class BaseCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T>? _canExecute;

        public BaseCommand(Action<T> execute) : this(execute, null)
        {
            _execute = execute;
        }

        public BaseCommand(Action<T> execute, Predicate<T>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute is not null && parameter is T tParameter)
            {
                return _canExecute(tParameter);
            }

            return _execute != null;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T tParameter)
                _execute?.Invoke(tParameter);
        }

        public event EventHandler? CanExecuteChanged = delegate { };
    }
}
