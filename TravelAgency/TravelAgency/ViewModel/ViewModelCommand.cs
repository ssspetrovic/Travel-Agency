﻿using System;
using System.Windows.Input;

namespace TravelAgency.ViewModel
{
    public class ViewModelCommand : ICommand
    {
        private readonly Action _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        public ViewModelCommand(Action executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null!;
        }

        public ViewModelCommand(Action executeAction, Predicate<object> canExecuteAction)
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
