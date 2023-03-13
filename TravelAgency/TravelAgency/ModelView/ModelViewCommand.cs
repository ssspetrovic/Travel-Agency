using System;
using System.Windows.Input;

namespace TravelAgency.ModelView
{
    class ModelViewCommand : ICommand
    {
        private readonly Action<Object> _executeAction;
        private readonly Action<Object> _canExecuteAction;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
