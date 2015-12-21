using System;
using System.Windows.Input;

namespace VisualQueryApplication
{
    public sealed class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public event CancelCommandEventHandler Executing;

        public event CommandEventHandler Executed;

        private Action action;
        private Action<object> parameterisedAction;

        public bool CanExecute
        {
            get
            {
                return canExecute;
            }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;

                    if (canExecuteChanged != null)
                        canExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool canExecute = false;

        public RelayCommand(Action<object> action)
        {
            this.parameterisedAction = action;
        }

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public void DoExecute(object param)
        {
            CancelCommandEventArgs args = new CancelCommandEventArgs() { Parameter = param, Cancel = false };
            InvokeExecuting(args);

            if (args.Cancel)
                return;

            InvokeAction(param);

            InvokeExecuted(new CommandEventArgs() { Parameter = param });
        }

        private void InvokeAction(object param)
        {
            if (action != null)
                action();
            else if (parameterisedAction != null)
                parameterisedAction(param);
        }

        private void InvokeExecuted(CommandEventArgs args)
        {
            if (Executed != null)
                Executed(this, args);
        }

        private void InvokeExecuting(CancelCommandEventArgs args)
        {
            if (Executing != null)
                Executing(this, args);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }

        void ICommand.Execute(object parameter)
        {
            this.DoExecute(parameter);
        }
    }

    public delegate void CommandEventHandler(object sender, CommandEventArgs args);

    public delegate void CancelCommandEventHandler(object sender, CancelCommandEventArgs args);

    public class CommandEventArgs : EventArgs
    {
        public object Parameter { get; set; }
    }

    public class CancelCommandEventArgs : CommandEventArgs
    {
        public bool Cancel { get; set; }
    }
}
