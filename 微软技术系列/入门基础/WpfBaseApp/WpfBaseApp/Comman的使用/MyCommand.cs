using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfBaseApp.Comman的使用
{
    public class MyCommand : ICommand
    {
        private readonly Action _action;
        public MyCommand(Action action) { 
            _action = action;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
}
