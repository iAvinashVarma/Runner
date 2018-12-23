﻿using RunnerBLL.Design;
using System;
using System.Diagnostics;
using System.Threading;

namespace RunnerBLL.Checker
{
	public class ProcessChecker : SingletonBase<ProcessChecker>
	{
		private static Mutex _mutex;

		public bool IsSingleInstance(string processName = "")
		{
			bool result = false;
			string name = string.IsNullOrEmpty(processName) ? AssemblyChecker.Instance.GUID : string.Format("{0}-{1}", AssemblyChecker.Instance.GUID, processName);
			try
			{
				Mutex.OpenExisting(name);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				_mutex = new Mutex(true, name);
				result = true;
			}
			return result;
		}
	}
}
