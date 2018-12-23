using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RunnerBLL.Model
{
	public class FileEntity
	{
		public FileEntity(string fileFullPath)
		{
			FileFullPath = Path.GetFullPath(fileFullPath);
		}

		public string FileFullPath
		{
			get; set;
		}

		public string DirectoryFullPath
		{
			get { return Path.GetDirectoryName(FileFullPath); }
		}

		public string FileNameWithoutExtension
		{
			get { return Path.GetFileNameWithoutExtension(FileFullPath); }
		}

		public string FileExtension
		{
			get { return Path.GetExtension(FileFullPath); }
		}

		public string FileData
		{
			get
			{
				var sb = new StringBuilder();
				var fileStream = new FileStream(FileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
				{
					string line;
					while ((line = streamReader.ReadLine()) != null)
					{
						sb.Append(line);
					}
				}
				return sb.ToString();
			}
		}

		public bool IsFileExists
		{
			get { return File.Exists(FileFullPath); }
		}

		public FileType FileType
		{
			get
			{
				Enum.TryParse(FileExtension.Replace(".", string.Empty).ToUpper(), out FileType fileType);
				return fileType;
			}
		}
	}
}
