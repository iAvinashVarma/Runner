using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RunnerBLL.Concrete
{
	public class DTExecUtility : ProcessUtility
	{
		public string DtsxPackagePath { get; set; }

		public Dictionary<string, string> UserVariables { get; set; }

		public DTExecUtility(string utilityPath) : base(utilityPath)
		{
			Initialize();
		}

		public DTExecUtility()
		{
			Initialize();
		}

		private void Initialize()
		{
			UserVariables = new Dictionary<string, string>();
		}

		public override string Arguments => GetArguments();

		private string GetArguments()
		{
			StringBuilder sb = new StringBuilder(string.Format("/FILE {0}", DtsxPackagePath));
			try
			{
				if (UserVariables.Any())
				{
					foreach (KeyValuePair<string, string> packageVariable in UserVariables)
					{
						sb.AppendFormat(@" /SET \Package.Variables[User::{0}].Properties[Value];\""{1}\""", packageVariable.Key, packageVariable.Value);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			return sb.ToString();
		}
	}
}
