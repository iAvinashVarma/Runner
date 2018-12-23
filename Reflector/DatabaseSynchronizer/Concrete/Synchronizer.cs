using DatabaseSynchronizer.Interface;
using RunnerBLL.Design;
using RunnerBLL.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSynchronizer.Concrete
{
	public class Synchronizer : SingletonBase<Synchronizer>, ISynchronizer
	{
		private List<ISynchronizer> _synchronizers = null;

		private Synchronizer()
		{
			_synchronizers = new List<ISynchronizer>();
		}

		public void RegisterObserver(ISynchronizer synchronizer)
		{
			if (!_synchronizers.Contains(synchronizer))
				_synchronizers.Add(synchronizer);
		}

		public void RegisterObserver(HashSet<ISynchronizer> synchronizers)
		{
			_synchronizers.ForEach(s =>
			{
				if (!_synchronizers.Contains(s))
					_synchronizers.Add(s);
			});
		}

		public void Run()
		{
			RunAllSynchronizers();
		}

		private void RunAllSynchronizers()
		{
			var createdTasks = new List<Task>();
			_synchronizers.ForEach(t =>
			{
				createdTasks.Add(Task.Factory.StartNew(() =>
				{
					t.Run();
				}));
			});

			Task.WaitAll(createdTasks.ToArray());
		}
	}
}
