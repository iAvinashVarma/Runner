namespace RunnerBLL.Interface
{
	public interface IRunnerObserver
	{
		bool IsEnabled { get; }

		int ObserverSequence { get; }

		void Run();
	}
}