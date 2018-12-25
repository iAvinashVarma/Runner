using log4net;
using RunnerBLL.Checker;
using RunnerBLL.Configuration;
using RunnerBLL.Design;
using RunnerBLL.Design.Factory;
using RunnerBLL.Extension;
using RunnerBLL.Interface;
using RunnerBLL.Model;
using RunnerBLL.Resources;
using RunnerBLL.Utility;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace RunnerBLL.Concrete
{
	public class ReflectorProcess : SingletonBase<ReflectorProcess>
	{
		private readonly RunnerWork<RunnerData> runnerWork = RunnerWork<RunnerData>.Instance;
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private Hashtable _hashtable = null;

		public void Run(Hashtable hashtable)
		{
			_hashtable = hashtable;
			string filePath = ApplicationSettings.Instance.FileSettings.Path;
			IDeserializer<RunnerEntity> deserializer = DataFactory.Instance.GetDeserializer<RunnerEntity>(filePath);
			RunnerEntity runner = deserializer.GetEntity();
			if (IsSingleProcess)
			{
				RunSingleProcess(runner);
			}
			else
			{
				RunMultiProcesses(runner);
			}
		}

		private bool IsSingleProcess => _hashtable.ContainsKey(Constants.Process);

		private void RunSingleProcess(RunnerEntity runnerEntity)
		{
			string process = _hashtable.GetValue(Constants.Process);
			if (!string.IsNullOrEmpty(process))
			{
				if (ProcessChecker.Instance.IsSingleInstance(process))
				{
					RunnerWork<RunnerData> runnerWork = RunnerWork<RunnerData>.Instance;
					if (runnerEntity.PreRunner != null && runnerEntity.PreRunner.PreRunnerDataList != null)
					{
						RunnerData runnerAssembly = runnerEntity.PreRunner.PreRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
						runnerWork.RegisterObserver(runnerAssembly);
					}
					if (runnerEntity.MainRunner != null && runnerEntity.MainRunner.MainRunnerDataList != null)
					{
						RunnerData runnerAssembly = runnerEntity.MainRunner.MainRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
						runnerWork.RegisterObserver(runnerAssembly);
					}
					if (runnerEntity.PostRunner != null && runnerEntity.PostRunner.PostRunnerDataList != null)
					{
						RunnerData runnerAssembly = runnerEntity.PostRunner.PostRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
						runnerWork.RegisterObserver(runnerAssembly);
					}
					if (runnerWork.Count > 0)
					{
						logger.InfoFormat(RunnerResource.GivenProcess, process);
						runnerWork.Run<IRunnerProcess>(_hashtable);
						logger.InfoFormat(RunnerResource.ProcessCompleted, process, runnerWork.Count);
					}
					else
					{
						logger.WarnFormat(RunnerResource.UnableToRegisterProcess, process);
					}
				}
				else
				{
					logger.WarnFormat(RunnerResource.ProcessAlreadyRunning, process);
				}
			}
			else
			{
				logger.Error(RunnerResource.InvalidProcess);
			}
		}

		private void RunMultiProcesses(RunnerEntity runnerEntity)
		{
			if (ProcessChecker.Instance.IsSingleInstance(Constants.AllProcesses))
			{
				RunnerWork<RunnerData> runnerWork = RunnerWork<RunnerData>.Instance;
				if (runnerEntity.PreRunner != null && runnerEntity.PreRunner.PreRunnerDataList != null)
				{
					runnerEntity.PreRunner.PreRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
					{
						runnerWork.RegisterObserver(r);
					});
				}
				if (runnerEntity.MainRunner != null && runnerEntity.MainRunner.MainRunnerDataList != null)
				{
					runnerEntity.MainRunner.MainRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
					{
						runnerWork.RegisterObserver(r);
					});
				}
				if (runnerEntity.PostRunner != null && runnerEntity.PostRunner.PostRunnerDataList != null)
				{
					runnerEntity.PostRunner.PostRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
					{
						runnerWork.RegisterObserver(r);
					});
				}
				if (runnerWork.Count > 0)
				{
					string processes = runnerWork.Entities.ToCSV(x => x.Process);
					logger.InfoFormat(RunnerResource.GivenProcesses, processes);
					runnerWork.Run<IRunnerProcess>(_hashtable);
					logger.InfoFormat(RunnerResource.ProcessesCompleted, processes);
				}
				else
				{
					logger.Warn(RunnerResource.UnableToRegisterProcesses);
				}
			}
			else
			{
				logger.Warn(RunnerResource.ApplicationAlreadyRunning);
			}
		}
	}
}