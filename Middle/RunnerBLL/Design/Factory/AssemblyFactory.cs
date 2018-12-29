﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RunnerBLL.Design.Factory
{
	public class AssemblyFactory : SingletonBase<AssemblyFactory>
	{
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
			string currentFolder = string.Format("{0}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			Assembly assembly = Assembly.LoadFrom(Path.Combine(currentFolder, string.Format("{0}.dll", assemblyName)));
			return GetInstance<T>(assembly, typeName);
		}

		public T GetInstance<T>(Assembly assembly, string typeName)
		{
			Type type = assembly.GetType(typeName, false, true);
			if (null != type.GetInterface(typeof(T).FullName))
			{
				T instance = (T)Activator.CreateInstance(type);
				return instance;
			}
			return default(T);
		}

		public IEnumerable<TSource> GetInstances<TSource, TExclude>()
		{
			return GetConcreteTypes<TSource>()
				.Where(f => null != f.GetInterface(typeof(TSource).FullName) && f.Name != typeof(TExclude).Name)
				.Select(s => (TSource)Activator.CreateInstance(s));
		}

		public IEnumerable<T> GetInstances<T>()
		{
			return GetConcreteTypes<T>()
				.Where(f => null != f.GetInterface(typeof(T).FullName))
				.Select(s => (T)Activator.CreateInstance(s));
		}

		public IEnumerable<Type> GetConcreteTypes<T>()
		{
			var type = typeof(T);
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(t => type.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
			return types;
		}
	}
}