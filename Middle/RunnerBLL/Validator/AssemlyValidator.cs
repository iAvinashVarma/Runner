using log4net;
using RunnerBLL.Checker;
using RunnerBLL.Interface;
using RunnerBLL.Resources;
using System.Collections;
using System.Reflection;

namespace RunnerBLL.Validator
{
	public class AssemlyValidator : IRunnerValidator
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public int ValidateSequence => 2;

		public bool IsValid(Hashtable hashtable)
		{
			bool isValid = true;
			logger.InfoFormat(RunnerResource.ApplicationInfo, AssemblyChecker.Instance.AssemblyName, AssemblyChecker.Instance.AssemblyFileVersion);
			return isValid;
		}
	}
}