using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
	class FileModel
	{
		public string Name { get; set; }
		public long Size { get; set; }
		public DateTime LastModified { get; set; }

		public FileModel(string path)
		{
			Name = Path.GetFileName(path);
			Size = new FileInfo(path).Length / 1024;
			LastModified = File.GetLastWriteTime(path);
		}
	}
}
