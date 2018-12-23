using DatabaseSynchronizer.Concrete;
using DatabaseSynchronizer.Interface;
using log4net;
using RunnerBLL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSynchronizer.Synchronize
{
	public class MSSQLToMongo : IRunnerProcess
	{
		public Hashtable Hashtable { get; set; }

		public void Start()
		{
			var synchronizer = Synchronizer.Instance;
			synchronizer.RegisterObserver(new CompanySynchronizer());
			synchronizer.RegisterObserver(new EmployeeSynchronizer());
			synchronizer.RegisterObserver(new StudentSynchronizer());
			synchronizer.Run();
		}

		public bool Validate()
		{
			return true;
		}
	}
}
