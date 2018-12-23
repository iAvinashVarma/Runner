using RunnerBLL.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RunnerBLL.Checker
{
	public class AssemblyChecker : SingletonBase<AssemblyChecker>
	{
		public string GUID
		{
			get
			{
				var assembly = typeof(AssemblyChecker).Assembly;
				var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
				return attribute.Value;
			}
		}
	}
}
