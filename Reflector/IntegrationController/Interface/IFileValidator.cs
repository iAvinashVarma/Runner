using System.Collections.Generic;

namespace IntegrationController.Interface
{
	public interface IFileValidator
	{
		bool IsValid { get; }

		Dictionary<string, string> UserVariables { get; }
	}
}
