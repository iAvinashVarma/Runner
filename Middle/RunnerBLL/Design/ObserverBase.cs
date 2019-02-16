using RunnerBLL.Extension;
using System.Collections.Generic;
using System.Linq;

namespace RunnerBLL.Design
{
	public abstract class ObserverBase<TObserver>
	{
		public HashSet<TObserver> Entities { get; set; } = new HashSet<TObserver>();

		public virtual void Attach(TObserver observer)
		{
			if (observer != null && !Entities.Contains(observer))
			{
				Entities.Add(observer);
			}
		}

		public virtual void Detach(TObserver observer)
		{
			Entities.RemoveWhere(e => observer != null && e.Equals(observer));
		}

		public virtual void Attach(IEnumerable<TObserver> observers)
		{
			observers.ForEach(o =>
			{
				if (!Entities.Any(e => e.Equals(o)))
				{
					Entities.Add(o);
				}
			});
		}

		public virtual void Detach(IEnumerable<TObserver> observers)
		{
			observers.ForEach(o =>
			{
				Entities.RemoveWhere(e => e.Equals(o));
			});
		}
	}
}