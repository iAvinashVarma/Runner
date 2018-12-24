using APIOperator.Concrete;
using RunnerBLL.Concrete;
using RunnerBLL.Interface;
using System.Collections;

namespace APIOperator.Operate
{
	public class APIWorker : IRunnerProcess
	{
		public Hashtable Hashtable { get; set; }

		public void Run()
		{
			var operation = new Operation();
			operation.RegisterObserver(new RegistrationOperation());
			operation.RegisterObserver(new LoginOperation());
			operation.Run();
		}

		public bool Validate()
		{
			return true;
		}
	}
}