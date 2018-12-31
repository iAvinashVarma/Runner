using DatabaseSynchronizer.Resources;
using log4net;
using RunnerBLL.Interface;
using System.Reflection;

namespace DatabaseSynchronizer.Concrete
{
	public class EmployeeSynchronizer : IRunnerObserver
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public int ObserverSequence => 4;

		public bool IsEnabled => true;

		public void Run()
		{
			logger.Info(DatabaseSynchronizerResource.EmployeeSynchronizer);
		}
	}
}