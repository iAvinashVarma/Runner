using DatabaseSynchronizer.Resources;
using log4net;
using RunnerBLL.Interface;
using System.Reflection;

namespace DatabaseSynchronizer.Concrete
{
	public class StudentSynchronizer : IRunnerObserver
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public int ObserverSequence => 1;

		public bool IsEnabled => true;

		public void Run()
		{
			logger.Info(DatabaseSynchronizerResource.StudentSynchronizer);
		}
	}
}