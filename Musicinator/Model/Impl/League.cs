using System;
using System.Linq;
using System.Collections.Generic;

namespace Musicinator.Model.Impl
{
	public class League : IGang
	{
		protected List<IGang> minionsEtc;

		public League (params IGang[] minions)
		{
			this.minionsEtc = new List<IGang> (minions);
			this.Duration = minions.Max (m => m.Duration);
		}

		public IMinion GetSacrificialMinion ()
		{
			return this.GetSoonestMinion ().GetSacrificialMinion ();
		}

		public void Reset ()
		{
			this.minionsEtc.ForEach (minion => minion.Reset ());
		}

		public long TimeToKill {
			get {
				return this.GetSoonestMinion ().TimeToKill;
			}
		}

		public long Duration { get; private set; }

		public bool FizzledOut {
			get {
				return this.minionsEtc.TrueForAll (m => m.FizzledOut);
			}
		}

		private IGang GetSoonestMinion ()
		{
			IGang soonest = null;
			long soonestTime = long.MaxValue;
			foreach (IGang minion in this.minionsEtc) {
				if (!minion.FizzledOut && minion.TimeToKill < soonestTime) {
					soonest = minion;
					soonestTime = minion.TimeToKill;
				}
			}
			return soonest;
			//return this.minionsEtc.Aggregate ((curMin, minion) => !minion.FizzledOut && (curMin == null || (minion.TimeToKill < curMin.TimeToKill)) ? minion : curMin);
		}
	}
}

