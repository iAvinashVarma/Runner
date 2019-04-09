using RunnerBLL.Configurator;
using RunnerBLL.Design;
using RunnerBLL.Extension;
using RunnerBLL.Validator;
using System.Collections;

namespace RunnerBLL.Concrete
{
	public class RunnerProcess : SingletonBase<RunnerProcess>
	{
		public void Run(string[] args)
		{
			Run(args.GetHashArguments());
		}

		private void Run(Hashtable hashtable)
		{
			RunnerConfigurator.Instance.Configure(hashtable);
			if (RunnerValidator.Instance.IsValid(hashtable))
			{
				ReflectorProcess.Instance.Run(hashtable);
			}
		}
	}
}