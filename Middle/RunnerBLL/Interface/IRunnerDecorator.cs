namespace RunnerBLL.Interface
{
	public interface IRunnerDecorator<T>
	{
		void Run(T entity);
	}
}