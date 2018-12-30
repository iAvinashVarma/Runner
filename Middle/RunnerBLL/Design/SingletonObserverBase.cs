using System;

namespace RunnerBLL.Design
{
	public abstract class SingletonObserverBase<TSource, TObserver> : ObserverBase<TObserver> where TSource : class, new()
	{
		private static readonly Lazy<TSource> instance = new Lazy<TSource>(() =>
		{
			return Activator.CreateInstance(typeof(TSource), true) as TSource;
		});

		public static TSource Instance => instance.Value;
	}
}