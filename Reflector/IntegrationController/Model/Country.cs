﻿using System;
using System.IO;

namespace IntegrationController.Model
{
	public class Country : FileIntegration
	{
		public Country(DirectoryInfo directory, DateTime dateTime) : base(directory, dateTime)
		{
		}

		public override FileIntegrationType FileIntegrationType => FileIntegrationType.Country;
	}
}
