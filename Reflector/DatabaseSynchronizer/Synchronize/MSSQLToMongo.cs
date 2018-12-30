using DatabaseSynchronizer.Concrete;
using RunnerBLL.Concrete;
using RunnerBLL.Interface;
using System.Collections;

namespace DatabaseSynchronizer.Synchronize
{
	public class MSSQLToMongo : IRunnerProcess
	{
		public void Run(Hashtable hashtable)
		{
			var synchronizer = new Synchronizer();
			synchronizer.Run(RunType.Parallel);
		}
	}
}