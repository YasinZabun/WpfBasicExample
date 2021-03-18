using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Utils.Base
{
   public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
