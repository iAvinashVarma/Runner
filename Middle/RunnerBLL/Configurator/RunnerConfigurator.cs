using RunnerBLL.Design;
using RunnerBLL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Configurator
{
	public class RunnerConfigurator : SingletonBase<RunnerConfigurator>, IRunnerConfigurator
	{
		public void Configure(Hashtable hashtable)
		{
			var runnerDictionary = new Dictionary<int, IRunnerConfigurator>
			{
				{ 1, new LoggerConfigurator() },
				{ 2, new CultureConfigurator() }
			};
			var runnerConfigurators = runnerDictionary.OrderBy(r => r.Key)
									.Select(s => s.Value);
			foreach (var runner in runnerConfigurators)
			{
				runner.Configure(hashtable);
			}
		}
	}
}
