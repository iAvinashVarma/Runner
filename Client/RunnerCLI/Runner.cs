using log4net;
using RunnerBLL.Checker;
using RunnerBLL.Concrete;
using RunnerBLL.Design;
using RunnerBLL.Design.Factory;
using RunnerBLL.Extension;
using RunnerBLL.Interface;
using RunnerBLL.Model;
using RunnerBLL.Utility;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace RunnerCLI
{
	public class Runner : SingletonBase<Runner>
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run(string[] args)
		{
			Run(args.GetHashArguments());
		}

		private void Run(Hashtable hashtable)
		{
			IDeserializer<RunnerEntity> deserializer = DataFactory.Instance.GetDeserializer<RunnerEntity>("runner.json");
			RunnerEntity runner = deserializer.GetEntity();
			if (hashtable.ContainsKey(Constants.Process))
			{
				string process = hashtable.Validate(Constants.Process);
				if (!string.IsNullOrEmpty(process))
				{
					if (ProcessChecker.Instance.IsSingleInstance(process))
					{
						RunnerWork<RunnerData> runnerWork = RunnerWork<RunnerData>.Instance;
						if (runner.PreRunner != null && runner.PreRunner.PreRunnerDataList != null)
						{
							RunnerData runnerAssembly = runner.PreRunner.PreRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
							runnerWork.RegisterObserver(runnerAssembly);
						}
						if (runner.MainRunner != null && runner.MainRunner.MainRunnerDataList != null)
						{
							RunnerData runnerAssembly = runner.MainRunner.MainRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
							runnerWork.RegisterObserver(runnerAssembly);
						}
						if (runner.PostRunner != null && runner.PostRunner.PostRunnerDataList != null)
						{
							RunnerData runnerAssembly = runner.PostRunner.PostRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
							runnerWork.RegisterObserver(runnerAssembly);
						}
						runnerWork.Run<IRunnerProcess>(hashtable);
					}
					else
					{
						logger.WarnFormat("{0} is already running.", process);
					}
				}
				else
				{
					logger.Error("Invalid Process.");
				}
			}
			else
			{
				if (ProcessChecker.Instance.IsSingleInstance(Constants.All))
				{
					if (runner.PreRunner != null && runner.PreRunner.PreRunnerDataList != null)
					{
						runner.PreRunner.PreRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
						{
							RunnerWork<RunnerData>.Instance.RegisterObserver(r);
						});
					}
					if (runner.MainRunner != null && runner.MainRunner.MainRunnerDataList != null)
					{
						runner.MainRunner.MainRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
						{
							RunnerWork<RunnerData>.Instance.RegisterObserver(r);
						});
					}
					if (runner.PostRunner != null && runner.PostRunner.PostRunnerDataList != null)
					{
						runner.PostRunner.PostRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
						{
							RunnerWork<RunnerData>.Instance.RegisterObserver(r);
						});
					}
					RunnerWork<RunnerData>.Instance.Run<IRunnerProcess>(hashtable);
				}
				else
				{
					logger.Warn("Application is already running.");
				}
			}
		}
	}
}