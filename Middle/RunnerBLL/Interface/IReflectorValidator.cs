using System.Collections;

namespace RunnerBLL.Interface
{
	public interface IReflectorValidator
	{
		int ValidateSequence { get; }

		bool IsValid(Hashtable hashtable);
	}
}