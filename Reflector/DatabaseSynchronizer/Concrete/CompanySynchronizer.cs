using DatabaseSynchronizer.Resources;
using log4net;
using RunnerBLL.Interface;
using System.Reflection;

namespace DatabaseSynchronizer.Concrete
{
	public class CompanySynchronizer : IRunnerObserver
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public int ObserverSequence => 5;

		public bool IsEnabled => true;

		public void Run()
		{
			logger.Info(DatabaseSynchronizerResource.CompanySynchronizer);
		}
	}
}