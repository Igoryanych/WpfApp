using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApp
{
	class ViewModel : INotifyPropertyChanged
	{
		private string selectedPath1, selectedPath2;
		private ICommand openDialogCommand;
		private RelayCommand compareFoldersCommand;

		public ObservableCollection<FileModel> _firstDirectory, _secondDirectory;

		public ViewModel()
		{
			compareFoldersCommand = new RelayCommand(FillViewList, PathsCorrect);
			_firstDirectory = new ObservableCollection<FileModel>();
			_secondDirectory = new ObservableCollection<FileModel>();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public string SelectedPath1
		{
			get
			{
				return selectedPath1;
			}
			set
			{
				if (selectedPath1 == value) return;
				selectedPath1 = value;
				OnPropertyChanged(nameof(selectedPath1));
				compareFoldersCommand.OnExecuteChanged();
			}
		}

		public string SelectedPath2
		{
			get
			{
				return selectedPath2;
			}
			set
			{
				if (selectedPath2 == value) return;
				selectedPath2 = value;
				OnPropertyChanged(nameof(selectedPath2));
				compareFoldersCommand.OnExecuteChanged();
			}
		}

		public ObservableCollection<FileModel> FirstDirectory
		{
			get
			{
				return _firstDirectory;
			}
		}

		public ObservableCollection<FileModel> SecondDirectory
		{
			get
			{
				return _secondDirectory;
			}
		}

		public ICommand OpenDialogCommand
		{
			get
			{
				if (openDialogCommand == null)
				{
					openDialogCommand = new RelayParametrizedCommand(
						param => OpenDialog(param.ToString()),
						() => { return true; }
						);
				}

				return openDialogCommand;
			}
		}

		public RelayCommand CompareFoldersCommand
		{
			get
			{
				return compareFoldersCommand;
			}
		}

		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public void OpenDialog(string path)
		{
			var dlg = new FolderBrowserDialog();
			DialogResult result = dlg.ShowDialog();
			
			if (result == DialogResult.OK)
			{
				if (path == "Path1")
				{
					SelectedPath1 = dlg.SelectedPath;
				}
				else
				{
					SelectedPath2 = dlg.SelectedPath;
				}
			}
		}

		private bool PathsCorrect()
		{
			if (!string.IsNullOrEmpty(selectedPath1) && !string.IsNullOrEmpty(selectedPath2))
			{
				if (Directory.Exists(selectedPath1) && Directory.Exists(selectedPath2))
				{
					return true;
				}
			}

			return false;
		}

		private void FillViewList()
		{
			PopulateCollection(_firstDirectory, Directory.GetFiles(SelectedPath1));

			PopulateCollection(_secondDirectory, Directory.GetFiles(SelectedPath2));
		}

		private void PopulateCollection(ObservableCollection<FileModel> directory, string[] files)
		{
			if (directory.Any())
			{
				directory.Clear();
			}

			foreach (string path in files)
			{
				directory.Add(new FileModel(path));
			}
		}
	}
}
