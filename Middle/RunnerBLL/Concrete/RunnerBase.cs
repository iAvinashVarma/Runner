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

		}

		public RunnerBase(Assembly assembly)
		{
			IOrderedEnumerable<IRunnerObserver> observers = AssemblyFactory.Instance.GetInstances<IRunnerObserver>(assembly)
							.Where(a => a.IsEnabled)
							.OrderBy(o => o.ObserverSequence);
			Attach(observers);
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

			if (createdTasks.Any())
			{
				Task.WaitAll(createdTasks.ToArray());
			}
		}

		public void Clear()
		{
			Entities.Clear();
		}
	}

	public enum RunType : byte
	{
		Sequential = 1,
		Parallel = 2
	}
}