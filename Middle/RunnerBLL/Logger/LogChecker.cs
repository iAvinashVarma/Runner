using log4net;
using log4net.Appender;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using RunnerBLL.Design;
using System.Linq;

namespace RunnerBLL.Logger
{
	public class LogChecker : SingletonBase<LogChecker>
	{
		public ILoggerRepository LoggerRepository => LogManager.GetRepository();

		public Hierarchy Hierarchy => (Hierarchy)LoggerRepository;

		public FileAppender FileAppender => Hierarchy.Root.Appenders.OfType<FileAppender>().FirstOrDefault();

		public string FilePath
		{
			get => FileAppender != null ? FileAppender.File : string.Empty;
			set
			{
				if (FileAppender != null)
				{
					FileAppender.File = value;
					FileAppender.ActivateOptions();
				}
			}
		}
	}
}