using System.Collections;

namespace RunnerBLL.Interface
{
	public interface IRunnerProcess
	{
		Hashtable Hashtable
		{
			get; set;
		}

		bool Validate();

		void Run();
	}
}