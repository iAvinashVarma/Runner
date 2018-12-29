using RunnerBLL.Design;
using RunnerBLL.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RunnerBLL.Validator
{
	public class RunnerValidator : SingletonBase<RunnerValidator>, IRunnerValidator
	{
		public bool IsValid(Hashtable hashtable)
		{
			bool isValid = false;
			var runnerDictionary = new Dictionary<int, IRunnerValidator>
			{
				{ 1, new CultureValidator() },
				{ 2, new AssemlyValidator() },
				{ 3, new LoggerValidator() }
			};
			var runnerValidators = runnerDictionary.OrderBy(r => r.Key)
									.Select(s => s.Value);
			foreach (var runner in runnerValidators)
			{
				isValid = runner.IsValid(hashtable);
				if (!isValid) break;
			}
			return isValid;
		}
	}
}
