using System;

namespace DatabaseSynchronizer.Model
{
	public class Employee : Person
	{
		public int Id { get; set; }

		public DateTime DateOfJoin { get; set; }
	}
}