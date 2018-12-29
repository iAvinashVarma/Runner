using log4net;
using RunnerBLL.Design;
using RunnerBLL.Design.Factory;
using RunnerBLL.Extension;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RunnerBLL.Concrete
{
	public class RunnerWork<T> : SingletonBase<RunnerWork<T>>
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public readonly List<T> Entities = new List<T>();

		public void RegisterObserver(T entity)
		{
			if (entity == null)
			{
				return;
			}

			if (!Entities.Contains(entity))
			{
				Entities.Add(entity);
			}
		}

		public void RegisterObserver(HashSet<T> entities)
		{
			entities.ForEach(e =>
			{
				if (!Entities.Contains(e))
				{
					Entities.Add(e);
				}
			});
		}

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
						logger.WarnFormat("Issue while running {0}.", e.ToString());
					}
				}
			});
		}

		public int Count => Entities.Count(e => e != null);
	}
}