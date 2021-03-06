﻿using RunnerBLL.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RunnerBLL.Checker
{
	public class AssemblyChecker : SingletonBase<AssemblyChecker>
	{
		private Assembly _assembly;

		public Assembly Assembly
		{
			get
			{
				_assembly = _assembly ?? Assembly.GetEntryAssembly();
				return _assembly;
			}
			set => _assembly = value;
		}

		public string GUID
		{
			get
			{
				Assembly assembly = typeof(AssemblyChecker).Assembly;
				GuidAttribute attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
				return attribute.Value;
			}
		}

		public string AssemblyName => Assembly.GetName().Name;

		public FileVersionInfo AssemblyFileVersionInfo => FileVersionInfo.GetVersionInfo(Assembly.Location);

		public string AssemblyFileVersion => AssemblyFileVersionInfo.FileVersion;

		public string AssemblyCopyright => AssemblyFileVersionInfo.LegalCopyright;
	}
}