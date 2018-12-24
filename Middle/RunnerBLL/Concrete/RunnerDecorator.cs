using RunnerBLL.Abstract;
using System.Collections;

namespace RunnerBLL.Concrete
{
	public class RunnerDecorator<T> : RunnerTemplate<T>
	{
		public RunnerDecorator(Hashtable hashtable) : base(hashtable)
		{
		}
	}
}