using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseSynchronizer.Model
{
	public class Company
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime Founded { get; set; }
	}
}
