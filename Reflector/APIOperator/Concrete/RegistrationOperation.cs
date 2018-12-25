﻿using log4net;
using RunnerBLL.Interface;
using RunnerBLL.Resources.Reflector;
using System.Reflection;

namespace APIOperator.Concrete
{
	public class RegistrationOperation : IRunnerObserver
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run()
		{
			logger.Info(APIOperatorResource.RegistrationOperation);
		}
	}
}