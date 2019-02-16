using System.Configuration;
using System.IO;
using System.Reflection;

namespace RunnerBLL.Configuration
{
	public class FileSettings : ConfigurationElement
	{
		private const string PathName = "Path";
		private const string DefaultPathValue = "runner.json";

		[ConfigurationProperty(PathName, DefaultValue = DefaultPathValue, IsRequired = true)]
		public string RunnerPath
		{
			get
			{
				string applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string pathName = (string)this[PathName];
				string fileSettingsPath = Path.GetFullPath(pathName);
				string filePath = File.Exists(fileSettingsPath) ? fileSettingsPath : Path.Combine(applicationDirectory, fileSettingsPath);
				return filePath;
			}
		}
	}
}