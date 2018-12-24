using log4net;
using System;
using System.Reflection;

namespace RunnerCLI
{
	internal class Program
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private static void Main(string[] args)
		{
			try
			{
				Runner.Instance.Run(args);
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}
			Console.ReadLine();
		}
	}
}