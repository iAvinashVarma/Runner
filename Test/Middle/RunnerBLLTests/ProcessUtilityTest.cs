using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunnerBLL.Concrete;

namespace RunnerBLLTests
{
	[TestClass]
	public class ProcessUtilityTest
	{
		[TestMethod]
		public void CheckIfDTExecUtilityIsHavingValidArguments()
		{
			string filePath = @"C:\PROGRA~2\MICROS~2\130\DTS\Binn\DTExec.exe";
			DTExecUtility utility = new DTExecUtility(filePath)
			{
				DtsxPackagePath = @"E:\Practice\SSIS\PackageSamples\FileTransform.dtsx"
			};
			utility.UserVariables.Add("inputFilePath", @"E:\Practice\SSIS\IntegrationPackages\CensusIntegration\Working\cbp02us.txt");
			utility.UserVariables.Add("everythingFilePath", @"E:\Practice\SSIS\IntegrationPackages\DataTransform\Output\EverythingCLI.csv");
			utility.UserVariables.Add("thresholdFilePath", @"E:\Practice\SSIS\IntegrationPackages\DataTransform\Output\ThresholdCLI.csv");
			utility.UserVariables.Add("zeroFilePath", @"E:\Practice\SSIS\IntegrationPackages\DataTransform\Output\ZeroCLI.csv");

			Assert.IsTrue(!string.IsNullOrEmpty(utility.Arguments));
		}
	}
}
