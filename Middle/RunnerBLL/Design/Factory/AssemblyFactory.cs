using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RunnerBLL.Design.Factory
{
	public class AssemblyFactory : SingletonBase<AssemblyFactory>
	{
		private AssemblyFactory() { }

		public T GetClass<T>(string className, string returnType)
		{
			return (T)Activator.CreateInstance(Type.GetType(Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith(className)).FirstOrDefault().FullName), new object[] { returnType });
		}

		public T GetReturnClass<T>(string className)
		{
			return (T)Assembly.Load(Assembly.GetExecutingAssembly().FullName).CreateInstance(Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith(className)).FirstOrDefault().FullName);
		}

		public T LoadAssembly<T>(string assemblyInfo)
		{
			try
			{
				if (assemblyInfo.IndexOf(',') != -1)
				{
					return LoadAssembly<T>(assemblyInfo.Split(',')[1], assemblyInfo.Split(',')[0]);
				}
				else
				{
					return LoadAssembly<T>(assemblyInfo.Substring(0, assemblyInfo.LastIndexOf('.')), assemblyInfo);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			return default(T);
		}

		public T LoadAssembly<T>(string assemblyName, string typeName)
		{
			var currentFolder = string.Format("{0}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			var assembly = Assembly.LoadFrom(Path.Combine(currentFolder, string.Format("{0}.dll", assemblyName)));
			return GetInstance<T>(assembly, typeName);
		}

		public T GetInstance<T>(Assembly assembly, string typeName)
		{
			var type = assembly.GetType(typeName, false, true);
			if (null != type.GetInterface(typeof(T).FullName))
			{
				var instance = (T)Activator.CreateInstance(type);
				return instance;
			}
			return default(T);
		}
	}
}
