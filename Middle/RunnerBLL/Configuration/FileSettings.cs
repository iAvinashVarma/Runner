using System.Configuration;

namespace RunnerBLL.Configuration
{
	public class FileSettings : ConfigurationElement
	{
		private const string PathName = "Path";
		private const string DefaultPathValue = "runner.json";

		[ConfigurationProperty(PathName, DefaultValue = DefaultPathValue, IsRequired = true)]
		public string Path => (string)this[PathName];
	}
}