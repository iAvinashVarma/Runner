using APIOperator.Concrete;
using log4net;
using RunnerBLL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace APIOperator.Operate
{
	public class APIWorker : IRunnerProcess
	{
		public Hashtable Hashtable { get; set; }

		public void Start()
		{
			var operation = Operation.Instance;
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
