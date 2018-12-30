using RunnerBLL.Interface;
using System.Collections;

namespace APIOperator.Validator
{
	public class OperatorValidator : IReflectorValidator
	{
		public int ValidateSequence => 1;

		public bool IsValid(Hashtable hashtable)
		{
			return true;
		}
	}
}