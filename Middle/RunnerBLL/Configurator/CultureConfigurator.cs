using RunnerBLL.Extension;
using RunnerBLL.Interface;
using RunnerBLL.Utility;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace RunnerBLL.Configurator
{
	public class CultureConfigurator : IRunnerConfigurator
	{
		public void Configure(Hashtable hashtable)
		{
			if (hashtable.ContainsKey(Constants.Culture))
			{
				string culture = hashtable.GetValue(Constants.Culture);
				if (!string.IsNullOrEmpty(culture))
				{
					Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
					Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
				}
			}
		}
	}
}
