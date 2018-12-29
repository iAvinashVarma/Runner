using log4net;
using log4net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Logger.Util
{
	public class PatternString : log4net.Util.PatternString
	{
		public override void ActivateOptions()
		{
			base.ActivateOptions();
		}
	}
}
