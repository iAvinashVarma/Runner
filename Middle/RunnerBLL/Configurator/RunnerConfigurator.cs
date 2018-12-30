using RunnerBLL.Design;
using RunnerBLL.Design.Factory;
using RunnerBLL.Interface;
using System.Collections;
using System.Linq;

namespace RunnerBLL.Configurator
{
	public class RunnerConfigurator : SingletonObserverBase<RunnerConfigurator, IRunnerConfigurator>, IRunnerConfigurator
	{
		public int ConfigureSequence => 0;

		public void Configure(Hashtable hashtable)
		{
			Attach(AssemblyFactory.Instance.GetInstances<IRunnerConfigurator, RunnerConfigurator>());
			IOrderedEnumerable<IRunnerConfigurator> sequenceRunnerConfigurators = Entities.OrderBy(r => r.ConfigureSequence);
			foreach (IRunnerConfigurator runner in sequenceRunnerConfigurators)
			{
				runner.Configure(hashtable);
			}
		}
	}
}