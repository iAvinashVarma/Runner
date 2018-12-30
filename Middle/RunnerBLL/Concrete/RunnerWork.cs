using log4net;
using RunnerBLL.Design;
using RunnerBLL.Design.Factory;
using RunnerBLL.Extension;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace RunnerBLL.Concrete
{
	public class RunnerWork<T> : SingletonObserverBase<RunnerWork<T>, T>
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run<P>(Hashtable hashtable)
		{
			Entities.ForEach(e =>
			{
				string runnerType = string.Format("{0}", e);
				if (e != null && !string.IsNullOrEmpty(runnerType))
				{
					P currentProcess = AssemblyFactory.Instance.LoadAssembly<P>(runnerType);
					if (currentProcess != null)
					{
						new RunnerDecorator<P>(hashtable).Run(currentProcess);
					}
					else
					{
						logger.WarnFormat("Issue while running {0}.", runnerType);
					}
				}
			});
		}

		public int Count => Entities.Count(e => e != null);
	}
}