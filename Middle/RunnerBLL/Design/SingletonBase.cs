using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Design
{
	public abstract class SingletonBase<T> where T : class
	{
		private static readonly Lazy<T> instance = new Lazy<T>(() =>
		{
			return Activator.CreateInstance(typeof(T), true) as T;
		});

		public static T Instance => instance.Value;
	}
}
