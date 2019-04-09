using System;

namespace RunnerBLL.Design
{
	public abstract class SingletonBase<T> where T : class, new()
	{
		protected SingletonBase()
		{

		}

		private static readonly Lazy<T> instance = new Lazy<T>(() =>
		{
			return Activator.CreateInstance(typeof(T), true) as T;
		});

		public static T Instance => instance.Value;
	}
}