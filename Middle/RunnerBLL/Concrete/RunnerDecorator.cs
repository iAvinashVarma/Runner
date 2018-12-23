using RunnerBLL.Abstract;
using RunnerBLL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Concrete
{
	public class RunnerDecorator<T> : RunnerTemplate<T>
	{
		public RunnerDecorator(Hashtable hashtable) : base(hashtable)
		{
		}
	}
}
