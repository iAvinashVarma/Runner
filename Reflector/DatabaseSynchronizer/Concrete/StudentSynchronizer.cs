﻿using log4net;
using RunnerBLL.Interface;
using RunnerBLL.Resources.Reflector;
using System.Reflection;

namespace DatabaseSynchronizer.Concrete
{
	public class StudentSynchronizer : IRunnerObserver
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run()
		{
			logger.Info(DatabaseSynchronizerResource.StudentSynchronizer);
		}
	}
}