using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TravelAgency.Command
{
    public abstract class BaseCommand : ICommand
    {
        public bool CanExecute(object? parameter)
        {
            // TODO
            return true;
        }

        public abstract void Execute(object? parameter);

        public event EventHandler? CanExecuteChanged;
    }
}
