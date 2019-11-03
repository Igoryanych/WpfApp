using System;
using System.Windows.Input;

namespace WpfApp
{
	class RelayCommand : ICommand
	{
		private Action _action;
		private Func<bool> _canExecute;

		public RelayCommand(Action action, Func<bool> canExecute)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			if (canExecute == null)
			{
				throw new ArgumentNullException(nameof(canExecute));
			}

			_action = action;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public void OnExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute();
		}

		public void Execute(object parameter)
		{
			_action();
		}
	}
}
