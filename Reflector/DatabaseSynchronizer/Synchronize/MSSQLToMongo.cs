using DatabaseSynchronizer.Concrete;
using RunnerBLL.Concrete;
using RunnerBLL.Interface;
using System.Collections;

namespace DatabaseSynchronizer.Synchronize
{
	public class MSSQLToMongo : IRunnerProcess
	{
		public Hashtable Hashtable { get; set; }

		public void Run()
		{
			var synchronizer = new Synchronizer();
			synchronizer.RegisterObserver(new CompanySynchronizer());
			synchronizer.RegisterObserver(new EmployeeSynchronizer());
			synchronizer.RegisterObserver(new StudentSynchronizer());
			synchronizer.Run(RunType.Parallel);
		}

		public bool Validate()
		{
			return true;
		}
	}
}