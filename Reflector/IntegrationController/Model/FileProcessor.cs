using IntegrationController.Interface;
using RunnerBLL.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IntegrationController.Model
{
	public class FileProcessor
	{
		private const string FilePath = @"C:\PROGRA~1\MICROS~1\150\DTS\Binn\DTExec.exe";
		private const string DtsxPackagePath = @"E:\Practice\SSIS\PackageSamples\FileTransform.dtsx";

		public void Start()
		{
			Dictionary<FileInfo, HashSet<IFileIntegration>> fileIntegrations = GetFileIntegrations();
			foreach (KeyValuePair<FileInfo, HashSet<IFileIntegration>> integration in fileIntegrations)
			{
				IFileValidator fileValidator = new FileValidator(integration.Value);
				if (fileValidator.IsValid)
				{
					ExecutePackage(fileValidator);
				}
			}
		}

		private void ExecutePackage(IFileValidator fileValidator)
		{
			DTExecUtility utility = new DTExecUtility(FilePath)
			{
				DtsxPackagePath = DtsxPackagePath,
				UserVariables = fileValidator.UserVariables
			};
			string result = string.Empty;
			int returnCode = utility.TryExecute(out result);
		}

		public Dictionary<FileInfo, HashSet<IFileIntegration>> GetFileIntegrations()
		{
			Dictionary<FileInfo, HashSet<IFileIntegration>> fileIntegrations = new Dictionary<FileInfo, HashSet<IFileIntegration>>();
			string censusDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Samples");
			DirectoryInfo directory = new DirectoryInfo(censusDirectory);
			List<FileInfo> files = directory.GetFiles(string.Format("{0}_*", FileIntegrationType.Continent)).ToList();
			files.ForEach(f =>
			{
				string[] datePart = Path.GetFileNameWithoutExtension(f.FullName).Split('_');
				if (datePart.Any() && datePart.Length > 0)
				{
					if (DateTime.TryParseExact(datePart[1], "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime asOfDate))
					{
						HashSet<IFileIntegration> hashSet = new HashSet<IFileIntegration>()
						{
							new Continent(directory, asOfDate),
							new Country(directory, asOfDate),
							new State(directory, asOfDate)
						};
						fileIntegrations.Add(f, hashSet);
					}
				}
			});
			return fileIntegrations;
		}
	}
}
