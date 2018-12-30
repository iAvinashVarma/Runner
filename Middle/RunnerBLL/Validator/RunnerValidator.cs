using RunnerBLL.Design;
using RunnerBLL.Interface;
using System.Collections;
using System.Linq;

namespace RunnerBLL.Validator
{
	public class RunnerValidator : SingletonObserverBase<RunnerValidator, IRunnerValidator>, IRunnerValidator
	{
		public int ValidateSequence => 0;

		public bool IsValid(Hashtable hashtable)
		{
			bool isValid = false;
			IOrderedEnumerable<IRunnerValidator> sequenceRunnerValidators = Entities.OrderBy(r => r.ValidateSequence);
			foreach (IRunnerValidator runner in sequenceRunnerValidators)
			{
				isValid = runner.IsValid(hashtable);
				if (!isValid)
				{
					break;
				}
			}
			return isValid;
		}
	}
}