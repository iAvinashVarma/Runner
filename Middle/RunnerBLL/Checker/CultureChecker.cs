using log4net;
using RunnerBLL.Design;
using RunnerBLL.Extension;
using RunnerBLL.Resources;
using RunnerBLL.Utility;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace RunnerBLL.Checker
{
	public class CultureChecker : SingletonBase<CultureChecker>
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Validator(Hashtable hashtable)
		{
			if(hashtable.ContainsKey(Constants.Culture))
			{
				string culture = hashtable.GetValue(Constants.Culture);
				if (!string.IsNullOrEmpty(culture))
				{
					logger.InfoFormat(RunnerResource.GivenCulture, culture);
					Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
					logger.InfoFormat(RunnerResource.CultureSet, culture);
				}
			}
		}
	}
}
