using NUnit.Framework;
using System.Collections.Generic;

namespace DataDrivenTests
{
	[TestFixture]
	public class CreditDecisionTests
	{
		public static IEnumerable<TestCaseData> CreditDecisionTestData
		{
			get
			{
				yield return new TestCaseData(100, "Declined");
				yield return new TestCaseData(549, "Declined");
				yield return new TestCaseData(550, "Maybe");
				yield return new TestCaseData(674, "Maybe");
				yield return new TestCaseData(675, "We look forward  to doing business with you!");
			}
		}

		[TestCaseSource(typeof(CreditDecisionTests), "CreditDecisionTestData")]
		public void MakeCreditDecision_Always_ReturnsExpectedResult(int creditScore, string expectedResult)
		{
			Assert.That(creditScore > 0);
		}
	}
}
