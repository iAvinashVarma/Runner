using RunnerBLL.Design;
using System.Runtime.InteropServices;

namespace RunnerBLL.Checker
{
	public class AssemblyChecker : SingletonBase<AssemblyChecker>
	{
		public string GUID
		{
			get
			{
				System.Reflection.Assembly assembly = typeof(AssemblyChecker).Assembly;
				GuidAttribute attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
				return attribute.Value;
			}
		}
	}
}