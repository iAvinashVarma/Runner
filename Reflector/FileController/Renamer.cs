using FileController.Resources;
using FileController.Utility;
using log4net;
using RunnerBLL.Extension;
using RunnerBLL.Interface;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace FileController
{
	public class Renamer : IRunnerProcess
	{
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public void Run(Hashtable Hashtable)
		{
			string filePath = string.Format("{0}", Hashtable[key: Constants.FilePath]);
			string extension = string.Format("{0}", Hashtable[key: Constants.Extension]);
			if (!new string[] { filePath, extension }.IsNullOrEmpty())
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
				FileInfo[] files = directoryInfo.GetFiles();
				Parallel.ForEach(files, file =>
				{
					string oldFileName = file.FullName;
					if (!Path.HasExtension(file.FullName))
					{
						string newFileName = string.Format("{0}.{1}", file.FullName, extension);
						File.Move(oldFileName, newFileName);
					}
				});
			}
			else
			{
				logger.Warn(FileControllerResource.ProvideDirectoryOrExtension);
			}
		}
	}
}
