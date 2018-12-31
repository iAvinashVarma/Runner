using APIOperator.Resources;
using log4net;
using RunnerBLL.Interface;
using System.Reflection;

namespace APIOperator.Concrete
{
	public class LoginOperation : IRunnerObserver
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public int ObserverSequence => 3;

		public bool IsEnabled => true;

		public void Run()
		{
			logger.Info(APIOperatorResource.LoginOperation);
		}
	}
}