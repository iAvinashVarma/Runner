using System;
using System.IO;

namespace IntegrationController.Model
{
	public class Continent : FileIntegration
	{
		public Continent(DirectoryInfo directory, DateTime dateTime) : base(directory, dateTime)
		{
		}

		public override FileIntegrationType FileIntegrationType => FileIntegrationType.Continent;
	}
}
