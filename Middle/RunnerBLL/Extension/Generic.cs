using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RunnerBLL.Extension
{
	/// <summary>
	/// Represents Object Extension Methods.
	/// </summary>
	public static class Generic
	{
		/// <summary>
		///  Check if object is null.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">Object to check.</param>
		/// <returns>True or false.</returns>
		public static bool IsNull<T>(this T source)
		{
			return source == null;
		}

		/// <summary>
		/// Convert string to enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T ParseEnum<T>(this string value)
		{
			T enumValue = (T)Enum.ToObject(typeof(T), 0);
			try
			{
				enumValue = (T)Enum.Parse(typeof(T), value, true);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
			return enumValue;
		}

		/// <summary>
		/// Check if the value is numeric or not.
		/// </summary>
		/// <typeparam name="T">Object to check.</typeparam>
		/// <param name="value">Value to check.</param>
		/// <returns>True or false.</returns>
		public static bool IsNumeric<T>(this T value)
		{
			return long.TryParse(value.ToString(), out long output);
		}

		/// <summary>
		/// Return the list in Alpah Numerical Length Wise
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="L"></typeparam>
		/// <param name="names"></param>
		/// <param name="lengthProvider"></param>
		/// <returns></returns>
		public static IEnumerable<T> AlphaLengthWise<T, L>(this IEnumerable<T> names, Func<T, L> lengthProvider)
		{
			return names.OrderBy(a => lengthProvider(a)).ThenBy(a => a);
		}

		public static string AddSpacesToSentence(this string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return string.Empty;
			}

			if (text.Length < 5)
			{
				return text;
			}

			StringBuilder newText = new StringBuilder(text.Length * 2);
			newText.Append(text[0]);
			for (int i = 1; i < text.Length; i++)
			{
				if (char.IsUpper(text[i]) && text[i - 1] != ' ')
				{
					newText.Append(' ');
				}

				newText.Append(text[i]);
			}
			return newText.ToString();
		}

		/// <summary>
		/// Filter value in a string array.
		/// </summary>
		/// <param name="arrayParamter">Array to check.</param>
		/// <param name="content">Content to filter.</param>
		/// <returns>Matching string array.</returns>
		public static string[] FilterArray(this string[] arrayParamter, string content)
		{
			return Array.FindAll(arrayParamter, contentPredicate => contentPredicate.IndexOf(content, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		public static bool IsTrue(this string value)
		{
			return value.Equals("Y", StringComparison.OrdinalIgnoreCase) || value.Equals("True", StringComparison.OrdinalIgnoreCase) || value.Equals("1", StringComparison.OrdinalIgnoreCase);
		}

		public static bool IsNullOrEmpty<T>(this T list)
		{
			bool isNullOrEmpty = true;
			if (list is IEnumerable values)
			{
				IEnumerator value = values.GetEnumerator();
				while (value.MoveNext())
				{
					isNullOrEmpty = value.Current.IsNull();
					if (!isNullOrEmpty)
					{
						isNullOrEmpty = string.IsNullOrEmpty(value.Current.ToString());
					}

					if (isNullOrEmpty)
					{
						break;
					}
				}
			}
			return isNullOrEmpty;
		}
	}
}