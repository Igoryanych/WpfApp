using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WpfApp
{
	class ViewModel : INotifyPropertyChanged
	{
		private string selectedPath;
		private ICommand openDialogCommand;

		public ViewModel()
		{
			openDialogCommand = new RelayCommand(OpenDialog);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public string SelectedPath
		{
			get
			{
				return selectedPath;
			}
			set
			{
				if (selectedPath == value) return;
				selectedPath = value;
				OnPropertyChanged(nameof(selectedPath));
			}
		}

		public ICommand OpenDialogCommand
		{
			get
			{
				return openDialogCommand;
			}
		}

		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public void OpenDialog()
		{
			var dlg = new FolderBrowserDialog();
			DialogResult result = dlg.ShowDialog();
			
			if (result == DialogResult.OK)
			{
				SelectedPath = dlg.SelectedPath;
			}
		}
	}
}
