﻿using RunnerBLL.Design;
using System.Configuration;

namespace RunnerBLL.Configuration
{
	public class ApplicationSettings : SingletonBase<ApplicationSettings>
	{
		private const string RunSettingsName = "RunSettings";

		public RunSettings RunSettings => ConfigurationManager.GetSection("RunSettings") as RunSettings;

		public FileSettings FileSettings => RunSettings.FileSettings;
	}
}