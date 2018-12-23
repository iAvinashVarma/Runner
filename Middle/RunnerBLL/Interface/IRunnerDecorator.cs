using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Interface
{
	public interface IRunnerDecorator<T>
	{
		void Run(T entity);
	}
}
