using IntegrationController.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntegrationController.Model
{
	public abstract class FileIntegration : IFileIntegration
	{
		private readonly FileInfo _file;
		private readonly DateTime _dateTime;

		public virtual FileInfo File => _file;

		public virtual DateTime AsOfDate => _dateTime;


		public virtual Dictionary<string, string> UserVariables { get; set; }

		public FileIntegration(DirectoryInfo directory, DateTime dateTime)
		{
			_file = directory.GetFiles(string.Format("{0}_{1:ddMMyyyy}.CSV", FileIntegrationType, dateTime), SearchOption.TopDirectoryOnly).FirstOrDefault();
			_dateTime = dateTime;
			Init();
		}

		private void Init()
		{
			if (IsValid)
			{
				UserVariables = new Dictionary<string, string>
				{
					{ string.Format("{0}FilePath", FileIntegrationType), File.FullName }
				};
			}
		}

		public abstract FileIntegrationType FileIntegrationType { get; }

		public virtual bool IsValid => File != null && File.Exists;
	}
}
