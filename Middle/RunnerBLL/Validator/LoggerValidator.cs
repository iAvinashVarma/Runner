using log4net;
using RunnerBLL.Interface;
using RunnerBLL.Logger;
using RunnerBLL.Resources.Logger;
using System.Collections;
using System.Reflection;

namespace RunnerBLL.Validator
{
	public class LoggerValidator : IRunnerValidator
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public int ValidateSequence => 3;

		public bool IsValid(Hashtable hashtable)
		{
			bool isValid = true;
			logger.InfoFormat(LoggerResource.LogFilePath, LogChecker.Instance.FilePath);
			return isValid;
		}
	}
}