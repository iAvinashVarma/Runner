using RunnerBLL.Design;
using System;
using System.Diagnostics;
using System.Threading;

namespace RunnerBLL.Checker
{
	public class ProcessChecker : SingletonBase<ProcessChecker>
	{
		public static Mutex Mutex { get; set; }

		public bool IsSingleInstance(string processName = "")
		{
			bool result = false;
			var guid = AssemblyChecker.Instance.GUID;
			string name = string.IsNullOrEmpty(processName) ? guid : string.Format("{0}-{1}", guid, processName);
			new Mutex(true, name, out result);
			return result;
		}
	}
}