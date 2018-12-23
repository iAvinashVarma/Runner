using APIOperator.Interface;
using RunnerBLL.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIOperator.Concrete
{
	public class Operation : SingletonBase<Operation>, IOperation
	{
		private List<IOperation> _operators = null;

		private Operation()
		{
			_operators = new List<IOperation>();
		}

		public void RegisterObserver(IOperation synchronizer)
		{
			if (!_operators.Contains(synchronizer))
				_operators.Add(synchronizer);
		}

		public void RegisterObserver(HashSet<IOperation> synchronizers)
		{
			_operators.ForEach(s =>
			{
				if (!_operators.Contains(s))
					_operators.Add(s);
			});
		}

		public void Run()
		{
			RunAllOperations();
		}

		private void RunAllOperations()
		{
			var createdTasks = new List<Task>();
			_operators.ForEach(t =>
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
