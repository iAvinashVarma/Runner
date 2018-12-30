using RunnerBLL.Extension;
using RunnerBLL.Interface;
using RunnerBLL.Logger;
using RunnerBLL.Utility;
using System.Collections;
using System.IO;

namespace RunnerBLL.Configurator
{
	public class LoggerConfigurator : IRunnerConfigurator
	{
		public int ConfigureSequence => 1;

		public void Configure(Hashtable hashtable)
		{
			if (hashtable.ContainsKey(Constants.Process))
			{
				string process = hashtable.GetValue(Constants.Process);
				if (!string.IsNullOrEmpty(process))
				{
					string logFilePath = LogChecker.Instance.FilePath;
					string fileDirectory = Path.GetDirectoryName(logFilePath);
					string updateLogFilePath = Path.Combine(fileDirectory, process);
					LogChecker.Instance.FilePath = updateLogFilePath;
				}
			}
		}
	}
}