using RunnerBLL.Concrete;
using RunnerBLL.Design;
using RunnerBLL.Extension;
using System.Collections;

namespace RunnerCLI
{
	public class Runner : SingletonBase<Runner>
	{
		public void Run(string[] args)
		{
			Run(args.GetHashArguments());
		}

		private void Run(Hashtable hashtable)
		{
			ReflectorProcess.Instance.Run(hashtable);
		}
	}
}