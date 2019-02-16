using RunnerBLL.Concrete;
using RunnerBLL.Interface;
using System.Collections.Generic;
using System.Reflection;

namespace APIOperator.Concrete
{
	public class APIOperation : RunnerBase
	{
		public APIOperation() : base(Assembly.GetCallingAssembly())
		{
		}

		public APIOperation(Assembly assembly) : base(assembly)
		{
		}

		public APIOperation(IEnumerable<IRunnerObserver> observers) : base(observers)
		{
		}
	}
}