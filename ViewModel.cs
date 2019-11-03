using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

namespace WpfApp
{
	class ViewModel : INotifyPropertyChanged
	{
		private string selectedPath1, selectedPath2;
		private ICommand openDialogCommand;

		public ObservableCollection<FileModel> _firstDirectory, _secondDirectory;

		public ViewModel()
		{
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
					openDialogCommand = new RelayCommand(param => OpenDialog(param.ToString()));
				}

				return openDialogCommand;
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
				string[] filePaths = Directory.GetFiles(dlg.SelectedPath);

				if (path == "Path1")
				{
					SelectedPath1 = dlg.SelectedPath;

					PopulateCollection(_firstDirectory, filePaths);
				}
				else
				{
					SelectedPath2 = dlg.SelectedPath;

					PopulateCollection(_secondDirectory, filePaths);
				}
			}
		}

		public void PopulateCollection(ObservableCollection<FileModel> directory, string[] files)
		{
			if (directory != null)
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
