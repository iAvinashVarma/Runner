using APIOperator.Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace APIOperator.Concrete
{
	public class LoginOperation : IOperation
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run()
		{
			logger.Info("Login Operation");
		}
	}
}
