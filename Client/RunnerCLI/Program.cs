using log4net;
using RunnerBLL.Concrete;
using System;
using System.Reflection;
using System.Resources;

[assembly: NeutralResourcesLanguage("en")]

namespace RunnerCLI
{
	internal class Program
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		protected Program() { }

		private static void Main(string[] args)
		{
			try
			{
				RunnerProcess.Instance.Run(args);
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}
		}
	}
}