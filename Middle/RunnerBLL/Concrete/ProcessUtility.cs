using System;
using System.Diagnostics;
using System.Text;

namespace RunnerBLL.Concrete
{
	public class ProcessUtility
	{
		public virtual string UtilityPath { get; set; }

		private string _outputData;

		private string _errorData;

		public string OutputData { get { return _outputData; } }

		public virtual string ErrorData { get { return _errorData; } }

		public ProcessUtility()
		{

		}

		public ProcessUtility(string utilityPath)
		{
			UtilityPath = utilityPath;
		}

		public virtual string Arguments { get; set; }

		public virtual int TryExecute(out string result)
		{
			StringBuilder sb = new StringBuilder();
			int exitCode = -1209;
			try
			{
				Process process = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = UtilityPath,
						Arguments = Arguments,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					}
				};
				process.OutputDataReceived += Process_OutputDataReceived;
				process.ErrorDataReceived += Process_ErrorDataReceived;
				process.Start();
				while (!process.StandardOutput.EndOfStream)
				{
					sb.AppendLine(process.StandardOutput.ReadLine());
				}
				process.WaitForExit();
				exitCode = process.ExitCode;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw ex;
			}
			result = sb.ToString();
			return exitCode;
		}

		private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			_outputData = e.Data;
		}

		private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			_errorData = e.Data;
		}
	}
}
