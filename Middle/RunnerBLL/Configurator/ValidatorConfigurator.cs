using RunnerBLL.Design.Factory;
using RunnerBLL.Interface;
using RunnerBLL.Validator;
using System.Collections;
using System.Collections.Generic;

namespace RunnerBLL.Configurator
{
	public class ValidatorConfigurator : IRunnerConfigurator
	{
		public int ConfigureSequence => 3;

		public void Configure(Hashtable hashtable)
		{
			IEnumerable<IRunnerValidator> observers = AssemblyFactory.Instance.GetInstances<IRunnerValidator, RunnerValidator>();
			RunnerValidator.Instance.Attach(observers);
		}
	}
}