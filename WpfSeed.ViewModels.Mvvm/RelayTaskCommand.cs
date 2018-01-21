using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfSeed.ViewModels.Mvvm
{
    public class RelayTaskCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        private bool _isTaskActive;

        public RelayTaskCommand(Func<Task> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = null;

            _isTaskActive = false;
        }
        public RelayTaskCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

            _isTaskActive = true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                ManualCanExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                ManualCanExecuteChanged -= value;
            }
        }
        private event EventHandler ManualCanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_isTaskActive)
                return false;
            else if (_canExecute == null)
                return true;
            else
                return _canExecute();
        }
        public void Execute(object parameter)
        {
            _isTaskActive = true;
            ManualCanExecuteChanged?.Invoke(this, EventArgs.Empty);

            var task = _execute();
            task.ContinueWith(t =>
            {
                _isTaskActive = false;
                ManualCanExecuteChanged?.Invoke(this, EventArgs.Empty);
            });
        }
    }

    public class RelayTaskCommand<TArg> : ICommand
    {
        private readonly Func<TArg, Task> _execute;
        private readonly Func<TArg, bool> _canExecute;

        private bool _isTaskActive;

        public RelayTaskCommand(Func<TArg, Task> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = null;

            _isTaskActive = true;
        }
        public RelayTaskCommand(Func<TArg, Task> execute, Func<TArg, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

            _isTaskActive = true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                ManualCanExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                ManualCanExecuteChanged -= value;
            }
        }
        private event EventHandler ManualCanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_isTaskActive)
                return false;
            else if (_canExecute == null)
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
            _isTaskActive = true;
            ManualCanExecuteChanged?.Invoke(this, EventArgs.Empty);

            Task task;
            if (parameter is null)
                task = _execute((TArg)parameter);
            else if (parameter is TArg arg)
                task = _execute(arg);
            else
                throw new ArgumentException("Unable to convert parameter to command type.", nameof(parameter));

            task.ContinueWith(t =>
            {
                _isTaskActive = false;
                ManualCanExecuteChanged?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
