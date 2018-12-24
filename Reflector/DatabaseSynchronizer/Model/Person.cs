using System;

namespace DatabaseSynchronizer.Model
{
	public class Person
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

		public Gender Gender { get; set; }
	}

	public enum Gender : byte
	{
		Unknown = 0,
		Male = 1,
		Female = 2
	}
}