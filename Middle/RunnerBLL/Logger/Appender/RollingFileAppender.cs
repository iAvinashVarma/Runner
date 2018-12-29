using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RunnerBLL.Logger.Appender
{
	public class RollingFileAppender : log4net.Appender.RollingFileAppender
	{
		private readonly string applicationName = Assembly.GetEntryAssembly().GetName().Name;

		/// <summary>
		/// Activate customer options on the file appender.
		/// </summary>
		public override void ActivateOptions()
		{
			if (string.IsNullOrEmpty(this.File))
			{
				File = Path.ChangeExtension(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, applicationName), "log");
			}
			base.ActivateOptions();
		}
	}
}
