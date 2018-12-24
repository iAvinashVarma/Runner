using System.Collections;

namespace RunnerBLL.Comparer
{
	public class InsensitiveComparer : IEqualityComparer
	{
		private CaseInsensitiveComparer _comparer = new CaseInsensitiveComparer();

		public int GetHashCode(object obj)
		{
			return obj.ToString().ToLowerInvariant().GetHashCode();
		}

		public new bool Equals(object x, object y)
		{
			if (_comparer.Compare(x, y) == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}