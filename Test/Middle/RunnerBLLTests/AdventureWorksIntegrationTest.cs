using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunnerBLL.Extension;
using RunnerBLLTests.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace RunnerBLLTests
{
	[TestClass]
	public class AdventureWorksIntegrationTest
	{
		[TestMethod]
		[ExpectedException(typeof(SqlException))]
		public void ConfirmDataSetLoad()
		{
			DataSet ds = GetReportHeaderDataSet();
			Assert.IsNotNull(ds);
			Assert.IsTrue(ds.Tables[0].Rows.Count > 0);
		}

		[TestMethod]
		[ExpectedException(typeof(SqlException))]
		public void ShouldConvertDataTypeRowToCsv()
		{
			DataSet ds = GetReportHeaderDataSet();
			List<string> list = new List<string>();
			if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
			{
				DataTable dataTable = ds.Tables[0];
				foreach (DataRow dataRow in dataTable.Rows)
				{
					list.Add(string.Format("{0}", dataRow["DataType"]));
				}
			}
			string csv = list.ToCSV(s => s, ";");
			Assert.IsNotNull(csv);
		}

		[TestMethod]
		[ExpectedException(typeof(SqlException))]
		public void CheckIfGivenDateIsInRange()
		{
			DataSet ds = GetReportDateLogDataSet();
			ReportDateLog reportDateLog = null;
			if (ds != null)
			{
				DataTable dataTable = ds.Tables[0];
				if (dataTable != null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
					DataRow dataRow = dataTable.Rows[0];
					reportDateLog = new ReportDateLog
					{
						Id = (int)dataRow["Id"],
						LastGeneratedTimestamp = (DateTime)dataRow["LastGeneratedTimestamp"],
						ReportFileTypeId = (int)dataRow["ReportFileTypeId"],
						TimeRange = (short)dataRow["TimeRange"]
					};
				}
			}
			if (reportDateLog != null)
			{
				DateTime startDate = reportDateLog.LastGeneratedTimestamp;
				DateTime endDate = reportDateLog.LastGeneratedTimestamp.AddMinutes(reportDateLog.TimeRange);
				DateTime currentDate = DateTime.Now;
				// Debug.WriteLine("{0:dd.MM.yyyy HH:mm:ss} {1:dd.MM.yyyy HH:mm:ss} {2:dd.MM.yyyy HH:mm:ss}", startDate, endDate, currentDate);
				if (currentDate > startDate && currentDate < endDate)
				{
					Debug.WriteLine("Should not run and skip the process.");
				}
				else
				{
					Debug.WriteLine("Can run.");
				}
			}
		}

		private DataSet GetReportHeaderDataSet()
		{
			string connection = ConfigurationManager.ConnectionStrings["AdventureWorksConnection"].ConnectionString;
			DataSet ds = new DataSet("ReportHeader");
			using (SqlConnection conn = new SqlConnection(connection))
			{
				SqlCommand sqlComm = new SqlCommand("[dbo].[usp_GetReportHeader]", conn)
				{
					CommandType = CommandType.StoredProcedure
				};
				sqlComm.Parameters.AddWithValue("@ReportFileTypeId", 1);
				sqlComm.Parameters.AddWithValue("@IsDebug", 1);

				SqlDataAdapter da = new SqlDataAdapter
				{
					SelectCommand = sqlComm
				};
				da.Fill(ds);
			}
			return ds;
		}

		private DataSet GetReportDateLogDataSet()
		{
			string connection = ConfigurationManager.ConnectionStrings["AdventureWorksConnection"].ConnectionString;
			DataSet ds = new DataSet("ReportHeader");
			using (SqlConnection conn = new SqlConnection(connection))
			{
				SqlCommand sqlComm = new SqlCommand("SELECT * FROM [dbo].[ReportDateLog] WHERE ReportFileTypeId = @ReportFileTypeId", conn)
				{
					CommandType = CommandType.Text
				};
				sqlComm.Parameters.AddWithValue("@ReportFileTypeId", 1);

				SqlDataAdapter da = new SqlDataAdapter
				{
					SelectCommand = sqlComm
				};
				da.Fill(ds);
			}
			return ds;
		}
	}
}
