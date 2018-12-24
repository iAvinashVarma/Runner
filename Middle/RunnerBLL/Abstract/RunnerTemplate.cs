using RunnerBLL.Interface;
using System.Collections;

namespace RunnerBLL.Abstract
{
	public abstract class RunnerTemplate<T> : IRunnerDecorator<T>
	{
		private IRunnerProcess _runnerProcess;
		private readonly Hashtable _hashtable;

		public RunnerTemplate(Hashtable hashtable)
		{
			_hashtable = hashtable;
		}

		public virtual void Run(T entity)
		{
			_runnerProcess = (IRunnerProcess)entity;
			if (_hashtable == null)
			{
				return;
			}

			_runnerProcess.Hashtable = _hashtable;
			if (_runnerProcess.Validate())
			{
				_runnerProcess.Run();
			}
		}
	}
}