using ExcelController.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RunnerBLL.Design.Factory;
using RunnerBLL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcelController
{
	public class MultiWorksheet : IRunnerProcess
	{
		public void Run(Hashtable Hashtable)
		{
			GetEmployees(out FileInfo fileInfo, out List<Employee> employees);
			GenerateWorksheets(fileInfo, employees);
		}

		private void GenerateWorksheets(FileInfo fileInfo, List<Employee> employees)
		{
			using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
			{
				const string templateSheetName = "Employee";
				AssignWorkbook(employees, excelPackage, templateSheetName);
				GenerateWorkbook(excelPackage, templateSheetName);
			}
		}

		private void GenerateWorkbook(ExcelPackage excelPackage, string templateSheetName)
		{
			using (FileStream file = File.OpenWrite($"Employee_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xlsx"))
			{
				excelPackage.Workbook.Worksheets.Delete(templateSheetName);
				excelPackage.SaveAs(file);
			}
		}

		private void AssignWorkbook(List<Employee> employees, ExcelPackage excelPackage, string templateSheetName)
		{
			ExcelWorkbook workBook = excelPackage.Workbook;

			foreach (Employee employee in employees)
			{
				ExcelWorksheet workSheet = AssignWorkSheet(templateSheetName, workBook, employee);
				AssignBorder(workSheet);
			}
		}

		private void GetEmployees(out FileInfo fileInfo, out List<Employee> employees)
		{
			fileInfo = new FileInfo(@"Model\Employee.xlsx");
			IDeserializer<Employees> deserializer = DataFactory.Instance.GetDeserializer<Employees>(@"Model\employees.xml");
			employees = deserializer.GetEntity().Employee;
		}

		private void AssignBorder(ExcelWorksheet workSheet)
		{
			string modelRange = $"A{1}:G{workSheet.Dimension.End.Row}";
			var modelTable = workSheet.Cells[modelRange];
			modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
			modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
			modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
			modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
		}

		private ExcelWorksheet AssignWorkSheet(string templateSheetName, ExcelWorkbook workBook, Employee employee)
		{
			ExcelWorksheet workSheet = Copy(workBook, templateSheetName, employee.Id.ToUpper());
			for (int rowId = 2; rowId < 1000; rowId++)
			{
				workSheet.Cells[rowId, 1].Value = $"{employee.FirstName}-{rowId}";
				workSheet.Cells[rowId, 2].Value = $"{employee.LastName}-{rowId}"; ;
				workSheet.Cells[rowId, 3].Value = employee.Title;
				workSheet.Cells[rowId, 4].Value = employee.Division;
				workSheet.Cells[rowId, 5].Value = employee.Building;
				workSheet.Cells[rowId, 6].Value = employee.Room;
				workSheet.Cells[rowId, 7].Value = string.IsNullOrEmpty(employee.Supervisor) ? "N/A" : employee.Supervisor;
			}
			//HideCells(workSheet);
			return workSheet;
		}

		private static void HideCells(ExcelWorksheet workSheet)
		{
			var column = workSheet.Column(workSheet.Dimension.End.Column + 1);
			var lastColumnFirstCell = workSheet.Cells.Last(c => c.Start.Column == 1);
			column.Hidden = true;
			var row = workSheet.Row(workSheet.Dimension.End.Row + 1);
			var lastRowFirstCell = workSheet.Cells.Last(c => c.Start.Row == 1);
			row.Hidden = true;
		}

		public ExcelWorksheet Copy(ExcelWorkbook workbook, string existingWorksheetName, string newWorksheetName)
		{
			ExcelWorksheet worksheet = workbook.Worksheets.Copy(existingWorksheetName, newWorksheetName);
			return worksheet;
		}
	}
}
