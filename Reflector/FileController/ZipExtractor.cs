using FileController.Resources;
using FileController.Utility;
using Ionic.Zip;
using log4net;
using RunnerBLL.Extension;
using RunnerBLL.Interface;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FileController
{
	public class ZipExtractor : IRunnerProcess
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run(Hashtable Hashtable)
		{
			string zipPath = string.Format("{0}", Hashtable[key: Constants.ZipPath]);
			string zipPassword = string.Format("{0}", Hashtable[key: Constants.ZipPassword]);
			if (!new string[] { zipPath }.IsNullOrEmpty())
			{
				FileInfo zipFileInfo = new FileInfo(zipPath);
				string outputDirectory = Path.Combine(zipFileInfo.Directory.FullName, string.Format("Extraction-{0:ddMMyyyyHHmmss}", DateTime.Now));
				ProcessStartInfo p = new ProcessStartInfo
				{
					FileName = @"C:\PROGRA~1\7-Zip\7z.exe",
					Arguments = string.IsNullOrEmpty(zipPassword) ? string.Format("x {0} -p{1} -o{2}", zipPath, zipPassword, outputDirectory) : string.Format("x {0} -o{1}", zipPath, outputDirectory),
					WindowStyle = ProcessWindowStyle.Hidden
				};
				Process x = Process.Start(p);
				x.WaitForExit();
				var code = x.ExitCode;
			}
			else
			{
				logger.Warn(FileControllerResource.ProvideZipProperties);
			}
		}
	}
}
