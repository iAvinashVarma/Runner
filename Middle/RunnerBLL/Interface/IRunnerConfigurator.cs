using System.Collections;

namespace RunnerBLL.Interface
{
	public interface IRunnerConfigurator
	{
		int ConfigureSequence { get; }

		void Configure(Hashtable hashtable);
	}
}