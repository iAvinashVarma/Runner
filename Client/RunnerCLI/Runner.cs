using RunnerBLL.Checker;
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
			CultureChecker.Instance.Validator(hashtable);
			ReflectorProcess.Instance.Run(hashtable);
		}
	}
}