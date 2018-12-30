using log4net;
using RunnerBLL.Interface;
using System.Collections;
using System.Reflection;

namespace RunnerBLL.Abstract
{
	public abstract class RunnerTemplate<T> : IRunnerDecorator<T>
	{
		private readonly Hashtable _hashtable;
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public RunnerTemplate(Hashtable hashtable)
		{
			_hashtable = hashtable;
		}

		public virtual void Run(T entity)
		{
			IRunnerProcess runnerProcess = entity as IRunnerProcess;
			if (runnerProcess != null)
			{
				runnerProcess.Run(_hashtable);
			}
		}
	}
}