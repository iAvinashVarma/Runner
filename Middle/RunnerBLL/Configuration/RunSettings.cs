using System.Configuration;

namespace RunnerBLL.Configuration
{
	public class RunSettings : ConfigurationSection
	{
		private const string FileSettingsName = "FileSettings";

		[ConfigurationProperty(FileSettingsName)]
		public FileSettings FileSettings => (FileSettings)this[FileSettingsName];
	}
}