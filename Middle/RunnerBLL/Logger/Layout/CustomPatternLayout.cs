using log4net.Layout;
using System;

namespace RunnerBLL.Logger.Layout
{
	public class CustomPatternLayout : PatternLayout
	{
		private readonly string line = string.Format("{0}{1}", new string('=', 96), Environment.NewLine);

		public override string Header
		{
			get
			{
				return line;
			}
		}

		public override string Footer
		{
			get
			{
				return line;
			}
		}
	}

}
