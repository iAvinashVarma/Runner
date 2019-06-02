using IntegrationController.Model;
using System;
using System.IO;

namespace IntegrationController.Interface
{
	public interface IFileIntegration : IFileValidator
	{
		FileIntegrationType FileIntegrationType { get; }

		DateTime AsOfDate { get; }

		FileInfo File { get; }
	}
}
