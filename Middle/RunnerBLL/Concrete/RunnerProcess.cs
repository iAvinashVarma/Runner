using log4net;
using RunnerBLL.Design;
using RunnerBLL.Design.Factory;
using RunnerBLL.Extension;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace RunnerBLL.Concrete
{
	public class RunnerWork<T> : SingletonBase<RunnerWork<T>>
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private List<T> _entities = null;

		private RunnerWork()
		{
			_entities = new List<T>();
		}

		public void RegisterObserver(T entity)
		{
			if (entity == null)
			{
				return;
			}

			if (!_entities.Contains(entity))
			{
				_entities.Add(entity);
			}
		}

		public void RegisterObserver(HashSet<T> entities)
		{
			entities.ForEach(e =>
			{
				if (!_entities.Contains(e))
				{
					_entities.Add(e);
				}
			});
		}

		public void Run<A>(Hashtable hashtable)
		{
			_entities.ForEach(e =>
			{
				string runnerType = string.Format("{0}", e);
				if (e != null && !string.IsNullOrEmpty(runnerType))
				{
					A currentProcess = AssemblyFactory.Instance.LoadAssembly<A>(runnerType);
					if (currentProcess != null)
					{
						new RunnerDecorator<A>(hashtable).Run(currentProcess);
					}
					else
					{
						logger.WarnFormat("Issue while running {0}.", e.ToString());
					}
				}
			});
		}
	}
}