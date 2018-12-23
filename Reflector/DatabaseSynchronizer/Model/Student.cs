using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseSynchronizer.Model
{
	public class Student : Person
	{
		public int Id { get; set; }

		public int Rank { get; set; }
	}
}
