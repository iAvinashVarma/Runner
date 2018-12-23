using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseSynchronizer.Model
{
	public class Employee : Person
	{
		public int Id { get; set; }

		public DateTime DateOfJoin { get; set; }
	}
}
