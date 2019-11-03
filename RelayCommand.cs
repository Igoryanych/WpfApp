using System;
using System.Windows.Input;

namespace WpfApp
{
	class RelayCommand : ICommand
	{
		private Action<object> _action;

		public RelayCommand(Action<object> passedAction)
		{
			_action = passedAction;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_action(parameter);
		}
	}
}
