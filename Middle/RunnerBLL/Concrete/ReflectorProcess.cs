using log4net;
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
using System.IO;
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
			string filePath = ApplicationSettings.Instance.FileSettings.RunnerPath;
			IDeserializer <RunnerEntity> deserializer = DataFactory.Instance.GetDeserializer<RunnerEntity>(filePath);
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

		private void RunMultiProcesses(RunnerEntity runnerEntity)
		{
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
	}
}