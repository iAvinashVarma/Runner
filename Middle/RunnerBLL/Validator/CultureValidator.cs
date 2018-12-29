using log4net;
using RunnerBLL.Design;
using RunnerBLL.Extension;
using RunnerBLL.Interface;
using RunnerBLL.Resources;
using RunnerBLL.Utility;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace RunnerBLL.Validator
{
	public class CultureValidator : IRunnerValidator
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public bool IsValid(Hashtable hashtable)
		{
			bool isValid = true;
			logger.InfoFormat(RunnerResource.CultureSet, Thread.CurrentThread.CurrentUICulture.NativeName);
			return isValid;
		}
	}
}
