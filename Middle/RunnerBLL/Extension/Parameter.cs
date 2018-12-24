using RunnerBLL.Comparer;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace RunnerBLL.Extension
{
	/// <summary>
	/// Represents parameter related functionality.
	/// </summary>
	public static class Parameter
	{
		/// <summary>
		/// Get the value of the parameter based on the name from the passed parameter list.
		/// </summary>
		/// <param name="additionalParameters">Parameters to pass.</param>
		/// <param name="parameterName">Parameter Name.</param>
		/// <returns>Parameter Value.</returns>
		public static string GetAdditionalParams(this string[] additionalParameters, string parameterName)
		{
			try
			{
				foreach (string param in additionalParameters.FilterArray(parameterName))
				{
					if ((param.Substring(0, 1).Equals("/") || param.Substring(0, 1).Equals("-")))
					{
						if (param.Contains("="))
						{
							return param.Substring(1, param.IndexOf("=") - 1).Equals(parameterName, StringComparison.OrdinalIgnoreCase) ? param.Substring(param.IndexOf("=") + 1) : string.Empty;
						}
						else
						{
							return param.Substring(1).Equals(parameterName, StringComparison.OrdinalIgnoreCase) ? param.Substring(1) : string.Empty;
						}
					}
				}
			}
			catch (Exception genericException)
			{
				Debug.WriteLine("{0}", genericException.Message);
			}

			return string.Empty;
		}

		public static Hashtable GetHashArguments(this string[] args)
		{
			Hashtable hashTable = new Hashtable(new InsensitiveComparer());
			foreach (string param in args)
			{
				try
				{
					if ((param.Substring(0, 1).Equals("/") || param.Substring(0, 1).Equals("-")))
					{
						if (param.Contains("="))
						{
							hashTable.Add(param.Substring(1, param.IndexOf("=") - 1), param.Substring(param.IndexOf("=") + 1));
						}
						else
						{
							hashTable.Add(param.Substring(1), true);
						}
					}
				}
				catch (Exception genericException)
				{
					Debug.WriteLine("{0}", genericException.Message);
				}
			}
			return hashTable;
		}

		/// <summary>
		/// Get the value of the parameter based on the occurence from the passed parameter list.
		/// </summary>
		/// <param name="additionalParameters">Parameters to pass.</param>
		/// <param name="occurrence">Parameter occurence.</param>
		/// <returns>Parameter Value.</returns>
		public static string GetAdditionalParams(this string[] additionalParameters, int occurrence)
		{
			try
			{
				if (occurrence <= additionalParameters.Length)
				{
					occurrence -= 1;
					string param = additionalParameters[occurrence];
					if (param.Substring(0, 1) == "/" || param.Substring(0, 1) == "-")
					{
						return param.Substring(1);
					}
				}
			}
			catch (Exception genericException)
			{
				Debug.WriteLine("{0}", genericException.InnerException);
			}
			return string.Empty;
		}

		public static string Validate(this Hashtable Hashtable, string key, bool isPath = false, string defaultValue = "")
		{
			object hash = Hashtable[key];
			string value = string.Empty;
			if (hash != null)
			{
				value = isPath ? Path.GetFullPath(hash.ToString()) : hash.ToString();
			}
			else if (!string.IsNullOrEmpty(defaultValue))
			{
				value = defaultValue;
			}
			else
			{
				throw new Exception(string.Format("Invalid {0} information.", key));
			}
			return value;
		}
	}
}