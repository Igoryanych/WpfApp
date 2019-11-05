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
		public bool HasUniqueName { get; set; } = true;
		public bool HasUniqueSize { get; set; } = true;
		public DateTime LastModified { get; set; }

		public FileModel(string path)
		{
			Name = Path.GetFileName(path);
			Size = new FileInfo(path).Length;
			LastModified = File.GetLastWriteTime(path);
		}
	}
}
