using RunnerBLL.Extension;
using RunnerBLL.Interface;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RunnerBLL.Concrete
{
	public class RunnerBase
	{
		private List<IRunnerObserver> _runnerObservers = null;

		public RunnerBase()
		{
			_runnerObservers = new List<IRunnerObserver>();
		}

		public void RegisterObserver(IRunnerObserver runnerObserver)
		{
			if (!_runnerObservers.Contains(runnerObserver))
			{
				_runnerObservers.Add(runnerObserver);
			}
		}

		public void RegisterObserver(HashSet<IRunnerObserver> runnerObservers)
		{
			runnerObservers.ForEach(s =>
			{
				if (!_runnerObservers.Any(e => e.Equals(s)))
				{
					_runnerObservers.Add(s);
				}
			});
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
			_runnerObservers.ForEach(e =>
			{
				if (e != null)
				{
					e.Run();
				}
			});
		}

		private void ParallelRun()
		{
			var currentCulture = Thread.CurrentThread.CurrentUICulture;
			List<Task> createdTasks = new List<Task>();
			_runnerObservers.ForEach(t =>
			{
				createdTasks.Add(Task.Factory.StartNew((cul) =>
				{
					var culture = cul as CultureInfo;
					if (t != null)
					{
						t.Run();
					}
				}, currentCulture));
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