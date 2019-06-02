using IntegrationController.Model;
using RunnerBLL.Concrete;
using RunnerBLL.Interface;
using System.Collections;
using System.Collections.Generic;

namespace IntegrationController
{
	public class DataTransform : IRunnerProcess
	{
		public void Run(Hashtable Hashtable)
		{
			var fileProcessor = new FileProcessor(Hashtable);
			fileProcessor.Start();
		}
	}
}
