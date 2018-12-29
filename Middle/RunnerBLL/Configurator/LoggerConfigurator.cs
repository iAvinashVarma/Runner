using RunnerBLL.Extension;
using RunnerBLL.Interface;
using RunnerBLL.Logger;
using RunnerBLL.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RunnerBLL.Configurator
{
	public class LoggerConfigurator : IRunnerConfigurator
	{
		public void Configure(Hashtable hashtable)
		{
			if (hashtable.ContainsKey(Constants.Process))
			{
				string process = hashtable.GetValue(Constants.Process);
				if (!string.IsNullOrEmpty(process))
				{
					var logFilePath = LogChecker.Instance.FilePath;
					var fileDirectory = Path.GetDirectoryName(logFilePath);
					var updateLogFilePath = Path.Combine(fileDirectory, process);
					LogChecker.Instance.FilePath = updateLogFilePath;
				}
			}
		}
	}
}
