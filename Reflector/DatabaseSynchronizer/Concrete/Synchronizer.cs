using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RunnerBLL.Concrete;
using RunnerBLL.Design.Factory;
using RunnerBLL.Interface;

namespace DatabaseSynchronizer.Concrete
{
	public class Synchronizer : RunnerBase
	{
		public Synchronizer() : base(Assembly.GetCallingAssembly())
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