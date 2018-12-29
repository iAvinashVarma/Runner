using log4net;
using log4net.Appender;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using RunnerBLL.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunnerBLL.Logger
{
	public class LogChecker : SingletonBase<LogChecker>
	{
		public ILoggerRepository LoggerRepository
		{
			get
			{
				return LogManager.GetRepository();
			}
		}

		public Hierarchy Hierarchy
		{
			get
			{
				return (Hierarchy)LoggerRepository;
			}
		}

		public FileAppender FileAppender
		{
			get
			{
				return Hierarchy.Root.Appenders.OfType<FileAppender>().FirstOrDefault();
			}
		}

		public string FilePath
		{
			get
			{
				return FileAppender != null ? FileAppender.File : string.Empty;
			}
			set
			{
				if(FileAppender != null)
				{
					FileAppender.File = value;
					FileAppender.ActivateOptions();
				}
			}
		}
	}
}
