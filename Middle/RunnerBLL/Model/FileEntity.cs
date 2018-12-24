using System;
using System.IO;
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

		public string DirectoryFullPath => Path.GetDirectoryName(FileFullPath);

		public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FileFullPath);

		public string FileExtension => Path.GetExtension(FileFullPath);

		public string FileData
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				FileStream fileStream = new FileStream(FileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
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

		public bool IsFileExists => File.Exists(FileFullPath);

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