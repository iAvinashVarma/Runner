using APIOperator.Concrete;
using RunnerBLL.Interface;
using System.Collections;

namespace APIOperator.Operate
{
	public class APIWorker : IRunnerProcess
	{
		public void Run(Hashtable hashtable)
		{
			Operation operation = new Operation();
			operation.Run();
		}
	}
}