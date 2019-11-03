using System;
using System.Windows.Input;

namespace WpfApp
{
	public class RelayParametrizedCommand : ICommand
	{
		private Action<object> _action;
		private Func<bool> _canExecute;

		public RelayParametrizedCommand(Action<object> passedAction, Func<bool> canExecute)
		{
			if (passedAction == null)
			{
				throw new ArgumentNullException(nameof(passedAction));
			}

			if (canExecute == null)
			{
				throw new ArgumentNullException(nameof(canExecute));
			}

			_action = passedAction;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return _canExecute();
		}

		public void Execute(object parameter)
		{
			_action(parameter);
		}
	}
}
