using System.Collections.Generic;
using System.Reflection;
using RunnerBLL.Concrete;
using RunnerBLL.Interface;

namespace DatabaseSynchronizer.Concrete
{
	public class Synchronizer : RunnerBase
	{
		public Synchronizer()
		{

		}

		public Synchronizer(IEnumerable<IRunnerObserver> observers) : base(observers)
		{
		}

		public Synchronizer(Assembly assembly) : base(assembly)
		{
			
		}
	}
}