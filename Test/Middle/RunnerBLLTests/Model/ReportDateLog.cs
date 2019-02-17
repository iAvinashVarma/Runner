using System;

namespace RunnerBLLTests.Model
{
	public class ReportDateLog
	{
		public int Id { get; set; }

		public int ReportFileTypeId { get; set; }

		public DateTime LastGeneratedTimestamp { get; set; }

		public short TimeRange { get; set; }
	}
}
