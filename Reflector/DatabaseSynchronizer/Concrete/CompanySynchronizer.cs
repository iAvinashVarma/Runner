using DatabaseSynchronizer.Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DatabaseSynchronizer.Concrete
{
	public class CompanySynchronizer : ISynchronizer
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run()
		{
			logger.Info("Synchronize Company");
		}
	}
}
