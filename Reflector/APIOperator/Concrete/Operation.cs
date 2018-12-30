using RunnerBLL.Concrete;
using RunnerBLL.Interface;
using System.Collections.Generic;
using System.Reflection;

namespace APIOperator.Concrete
{
	public class Operation : RunnerBase
	{
		public Operation()
		{

		}

		public Operation(Assembly assembly) : base(assembly)
		{
		}

		public Operation(IEnumerable<IRunnerObserver> observers) : base(observers)
		{
		}
	}
}