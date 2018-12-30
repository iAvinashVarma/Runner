using log4net;
using RunnerBLL.Checker;
using RunnerBLL.Concrete;
using RunnerBLL.Configuration;
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

namespace RunnerBLL.Validator
{
	public class ProcessValidator : IRunnerValidator
	{
		private static Hashtable _hashtable;
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly RunnerWork<RunnerData> runnerWork = RunnerWork<RunnerData>.Instance;

		public int ValidateSequence => 4;

		private bool IsSingleProcess => _hashtable.ContainsKey(Constants.Process);

		public bool IsValid(Hashtable hashtable)
		{
			bool isValid = true;
			_hashtable = hashtable;
			string filePath = ApplicationSettings.Instance.FileSettings.Path;
			IDeserializer<RunnerEntity> deserializer = DataFactory.Instance.GetDeserializer<RunnerEntity>(filePath);
			RunnerEntity runner = deserializer.GetEntity();
			if (IsSingleProcess)
			{
				isValid = RegisterSingleProcess(runner);
			}
			else
			{
				isValid = RegisterMultiProcesses(runner);
			}
			return isValid;
		}

		private bool RegisterSingleProcess(RunnerEntity runnerEntity)
		{
			bool isValid = true;
			string process = _hashtable.GetValue(Constants.Process);
			if (!string.IsNullOrEmpty(process))
			{
				if (ProcessChecker.Instance.IsSingleInstance(process))
				{
					if (runnerEntity.PreRunner != null && runnerEntity.PreRunner.PreRunnerDataList != null)
					{
						RunnerData runnerAssembly = runnerEntity.PreRunner.PreRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
						runnerWork.Attach(runnerAssembly);
					}
					if (runnerEntity.MainRunner != null && runnerEntity.MainRunner.MainRunnerDataList != null)
					{
						RunnerData runnerAssembly = runnerEntity.MainRunner.MainRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
						runnerWork.Attach(runnerAssembly);
					}
					if (runnerEntity.PostRunner != null && runnerEntity.PostRunner.PostRunnerDataList != null)
					{
						RunnerData runnerAssembly = runnerEntity.PostRunner.PostRunnerDataList.FirstOrDefault(r => r.Enable && r.Process.Equals(process, StringComparison.OrdinalIgnoreCase));
						runnerWork.Attach(runnerAssembly);
					}
					if (runnerWork.Count == 0)
					{
						isValid = false;
						logger.WarnFormat(RunnerResource.ProcessIsInvalid, process);
					}
				}
				else
				{
					isValid = false;
					logger.WarnFormat(RunnerResource.ProcessAlreadyRunning, process);
				}
			}
			else
			{
				isValid = false;
				logger.Error(RunnerResource.InvalidProcess);
			}
			return isValid;
		}

		private bool RegisterMultiProcesses(RunnerEntity runnerEntity)
		{
			bool isValid = true;
			if (ProcessChecker.Instance.IsSingleInstance(Constants.AllProcesses))
			{
				if (runnerEntity.PreRunner != null && runnerEntity.PreRunner.PreRunnerDataList != null)
				{
					runnerEntity.PreRunner.PreRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
					{
						runnerWork.Attach(r);
					});
				}
				if (runnerEntity.MainRunner != null && runnerEntity.MainRunner.MainRunnerDataList != null)
				{
					runnerEntity.MainRunner.MainRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
					{
						runnerWork.Attach(r);
					});
				}
				if (runnerEntity.PostRunner != null && runnerEntity.PostRunner.PostRunnerDataList != null)
				{
					runnerEntity.PostRunner.PostRunnerDataList.Where(r => r.Enable).OrderBy(r => r.Sequence).ToList().ForEach(r =>
					{
						runnerWork.Attach(r);
					});
				}
			}
			else
			{
				isValid = false;
				logger.Warn(RunnerResource.ApplicationAlreadyRunning);
			}
			return isValid;
		}
	}
}