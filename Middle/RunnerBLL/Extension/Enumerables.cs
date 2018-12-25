using System;
using System.Collections.Generic;
using System.Linq;

namespace RunnerBLL.Extension
{
	public static class Enumerables
	{
		public static void ForEach<TSource>(this IEnumerable<TSource> @enumeration, Action<TSource> action)
		{
			foreach (TSource item in @enumeration)
			{
				action(item);
			}
		}

		public static T DoAction<T>(this T @this, Action<string> action)
		{
			action.Invoke(null);
			return @this;
		}

		public static string ToCSV<TSource>(this IEnumerable<TSource> @enumeration, Func<TSource, string> selector)
		{
			return string.Join(", ", @enumeration.Select(selector).ToArray());
		}
	}
}