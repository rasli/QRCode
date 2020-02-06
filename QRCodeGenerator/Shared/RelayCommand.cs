using System;
using System.Windows.Input;

namespace QRGenerator.Shared
{
    public class RelayCommand : ICommand
    {
        private Action action;
        private Func<bool> canExecuteAction;
        public event EventHandler CanExecuteChanged = delegate { };
        public RelayCommand(Action action)
        {
            this.action = action;
        }
        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            action = executeMethod;
            canExecuteAction = canExecuteMethod;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        void ICommand.Execute(object parameter)
        {
            action?.Invoke();
        }
        bool ICommand.CanExecute(object parameter)
        {
            if (canExecuteAction != null)
            {
                return canExecuteAction();
            }
            if (action != null)
            {
                return true;
            }
            return false;
        }
    }
    public class RelayCommand<Model> : ICommand
    {
        private Action<Model> action;
        private Func<Model, bool> canExecuteAction;
        public event EventHandler CanExecuteChanged = delegate { };
        public RelayCommand(Action<Model> action)
        {
            this.action = action;
        }
        public RelayCommand(Action<Model> executeMethod, Func<Model, bool> canExecuteMethod)
        {
            action = executeMethod;
            canExecuteAction = canExecuteMethod;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        void ICommand.Execute(object parameter)
        {
            if (action != null && parameter is Model)
            {
                action((Model)parameter);
            }
        }
        bool ICommand.CanExecute(object parameter)
        {
            if (canExecuteAction != null && parameter is Model)
            {
                return canExecuteAction((Model)parameter);
            }
            if (action != null && parameter is Model)
            {
                return true;
            }
            return false;
        }
    }
}
