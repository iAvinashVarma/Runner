using IntegrationController.Interface;
using RunnerBLL.Extension;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationController.Model
{
	public class FileValidator : IFileValidator
	{
		public HashSet<IFileIntegration> _fileProcesses;

		public FileValidator(HashSet<IFileIntegration> fileProcesses)
		{
			_fileProcesses = fileProcesses;
		}

		public bool IsValid
		{
			get
			{
				foreach (IFileIntegration fileProcess in _fileProcesses)
				{
					if (!fileProcess.IsValid)
					{
						return false;
					}
				}
				return true;
			}
		}

		public Dictionary<string, string> UserVariables
		{
			get
			{
				var asOfDateKey = "AsOfDate";
				var runIdKey = "RunId";
				var userVariables = new Dictionary<string, string>();
				AddRunIdUserVariable(runIdKey, userVariables);
				foreach (IFileIntegration fileProcess in _fileProcesses)
				{
					if (fileProcess.UserVariables != null && fileProcess.UserVariables.Any())
					{
						userVariables.AddRange(fileProcess.UserVariables);
						AddAsOfDataUserVariable(asOfDateKey, userVariables, fileProcess);
					}
				}
				return userVariables;
			}
		}

		private void AddAsOfDataUserVariable(string asOfDateKey, Dictionary<string, string> userVariables, IFileIntegration fileProcess)
		{
			if (!userVariables.TryGetValue(asOfDateKey, out string asOfDateValue))
			{
				userVariables.Add(asOfDateKey, fileProcess.AsOfDate.ToString("dd-MM-yyyy"));
			}
		}

		private void AddRunIdUserVariable(string runIdKey, Dictionary<string, string> userVariables)
		{
			if (!userVariables.TryGetValue(runIdKey, out string runValue))
			{
				userVariables.Add(runIdKey, "1");
			}
		}
	}
}
