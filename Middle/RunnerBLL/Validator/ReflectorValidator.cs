using RunnerBLL.Concrete;
using RunnerBLL.Design.Factory;
using RunnerBLL.Interface;
using RunnerBLL.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RunnerBLL.Validator
{
	public class ReflectorValidator : IRunnerValidator
	{
		private static readonly RunnerWork<RunnerData> runnerWork = RunnerWork<RunnerData>.Instance;

		public int ValidateSequence => 5;

		public bool IsValid(Hashtable hashtable)
		{
			bool isValid = true;
			IEnumerable<string> assemblies = runnerWork.Entities.Select(e => e.AssemblyName);
			foreach (string assembly in assemblies)
			{
				IEnumerable<IReflectorValidator> instances = AssemblyFactory.Instance.GetInstances<IReflectorValidator>(assembly);
				IOrderedEnumerable<IReflectorValidator> sequenceInstances = instances.OrderBy(i => i.ValidateSequence);
				foreach (IReflectorValidator instance in sequenceInstances)
				{
					isValid = instance.IsValid(hashtable);
					if (!isValid)
					{
						break;
					}
				}
			}
			return isValid;
		}
	}
}