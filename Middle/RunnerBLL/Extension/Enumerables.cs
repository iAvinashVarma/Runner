using System;
using System.Collections.Generic;
using System.Linq;

namespace RunnerBLL.Extension
{
	public static class Enumerables
	{
		public static void ForEach<TSource>(this IEnumerable<TSource> @enumeration, Action<TSource> action)
		{
			if (@enumeration != null)
			{
				foreach (TSource item in @enumeration)
				{
					if (item != null)
					{
						action(item);
					}
				}
			}
		}

		public static T DoAction<T>(this T @this, Action<string> action)
		{
			if (@this != null)
			{
				action.Invoke(null);
			}

			return @this;
		}

		public static string ToCSV<TSource>(this IEnumerable<TSource> @enumeration, Func<TSource, string> selector)
		{
			string result = string.Empty;

			if (@enumeration != null)
			{
				result = string.Join(", ", @enumeration.Select(selector).ToArray());
			}

			return result;
		}
	}
}