using RunnerBLL.Design;
using System.Globalization;
using System.Threading;

namespace RunnerBLL.Checker
{
	public class CultureChecker : SingletonBase<CultureChecker>
	{
		private static readonly CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
		private static readonly CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;

		public CultureInfo CurrentCulture => currentCulture;

		public CultureInfo CurrentUICulture => currentUICulture;
	}
}