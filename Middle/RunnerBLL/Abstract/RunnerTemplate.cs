using RunnerBLL.Interface;
using System.Collections;

namespace RunnerBLL.Abstract
{
	public abstract class RunnerTemplate<T> : IRunnerDecorator<T>
	{
		private readonly Hashtable _hashtable;

		protected RunnerTemplate(Hashtable hashtable)
		{
			_hashtable = hashtable;
		}

		public virtual void Run(T entity)
		{
			if (entity is IRunnerProcess runnerProcess)
			{
				runnerProcess.Run(_hashtable);
			}
		}
	}
}