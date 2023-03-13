﻿using System;
using System.Windows.Input;

namespace TravelAgency.ModelView
{
    public class ModelViewCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        public ModelViewCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null!;
        }

        public ModelViewCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
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
            if (parameter != null) _executeAction(parameter);
        }
        
    }
}
