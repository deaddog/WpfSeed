using System;
using System.Windows.Input;

namespace WpfSeed.ViewModels.Mvvm
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = null;
        }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }
        public void Execute(object parameter)
        {
            _execute();
        }
    }

    public class RelayCommand<TArg> : ICommand
    {
        private readonly Action<TArg> _execute;
        private readonly Func<TArg, bool> _canExecute;

        public RelayCommand(Action<TArg> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = null;
        }
        public RelayCommand(Action<TArg> execute, Func<TArg, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            else if (parameter is null)
                return _canExecute((TArg)parameter);
            else if (parameter is TArg arg)
                return _canExecute(arg);
            else
                throw new ArgumentException("Unable to convert parameter to command type.", nameof(parameter));
        }
        public void Execute(object parameter)
        {
            if (parameter is null)
                _execute((TArg)parameter);
            else if (parameter is TArg arg)
                _execute(arg);
            else
                throw new ArgumentException("Unable to convert parameter to command type.", nameof(parameter));
        }
    }
}
