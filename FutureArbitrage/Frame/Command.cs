using System;
using System.Windows.Input;

namespace FutureArbitrage.Frame
{
    public class Command : ICommand
    {
        private Action action;

        private Action<object> parameterizedAction;

        public event EventHandler CanExecuteChanged;

        public Command(Action action)
        {
            this.action = action;
        }

        public Command(Action<object> parameterizedAction)
        {
            this.parameterizedAction = parameterizedAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (this.action != null)
            {
                this.action();
            }
            else
            {
                this.parameterizedAction(parameter);
            }
        }
    }
}