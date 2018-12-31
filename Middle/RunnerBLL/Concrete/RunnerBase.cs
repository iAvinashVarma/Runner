using RunnerBLL.Checker;
using RunnerBLL.Design;
using RunnerBLL.Design.Factory;
using RunnerBLL.Extension;
using RunnerBLL.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RunnerBLL.Concrete
{
	public abstract class RunnerBase : ObserverBase<IRunnerObserver>
	{
		public RunnerBase()
		{
			var assembly = Assembly.GetCallingAssembly();
			var assemblies = AssemblyFactory.Instance.GetInstances<IRunnerObserver>(assembly)
							.Where(a => a.IsEnabled)
							.OrderBy(o => o.ObserverSequence);
			Attach(assemblies);
		}

		public RunnerBase(Assembly assembly)
		{
			var assemblies = AssemblyFactory.Instance.GetInstances<IRunnerObserver>(assembly)
							.Where(a => a.IsEnabled)
							.OrderBy(o => o.ObserverSequence);
			Attach(assemblies.Where(a => a.IsEnabled).OrderBy(o => o.ObserverSequence));
		}

		public RunnerBase(IEnumerable<IRunnerObserver> observers)
		{
			Attach(observers);
		}

		public virtual void Run(RunType runType = RunType.Sequential)
		{
			switch (runType)
			{
				case RunType.Sequential:
					SequentialRun();
					break;

				case RunType.Parallel:
					ParallelRun();
					break;

				default:
					SequentialRun();
					break;
			}
		}

		private void SequentialRun()
		{
			Entities.ForEach(e =>
			{
				if (e != null)
				{
					e.Run();
				}
			});
		}

		private void ParallelRun()
		{
			List<Task> createdTasks = new List<Task>();
			Entities.ForEach(t =>
			{
				createdTasks.Add(Task.Factory.StartNew(() =>
				{
					Thread.CurrentThread.CurrentCulture = CultureChecker.Instance.CurrentCulture;
					Thread.CurrentThread.CurrentUICulture = CultureChecker.Instance.CurrentUICulture;
					if (t != null)
					{
						t.Run();
					}
				}));
			});

			Task.WaitAll(createdTasks.ToArray());
		}
	}

	public enum RunType : byte
	{
		Sequential = 1,
		Parallel = 2
	}
}