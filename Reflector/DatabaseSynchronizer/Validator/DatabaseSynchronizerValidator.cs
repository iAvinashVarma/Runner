using RunnerBLL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseSynchronizer.Validator
{
	public class DatabaseSynchronizerValidator : IReflectorValidator
	{
		public int ValidateSequence => 1;

		public bool IsValid(Hashtable hashtable)
		{
			return true;
		}
	}
}
