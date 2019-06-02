using System;
using System.IO;

namespace IntegrationController.Model
{
	public class State : FileIntegration
	{
		public State(DirectoryInfo directory, DateTime dateTime) : base(directory, dateTime)
		{
		}

		public override FileIntegrationType FileIntegrationType => FileIntegrationType.State;
	}
}
