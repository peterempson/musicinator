using System;
using System.Collections.Generic;
using Musicinator.Model;
using System.Linq;

namespace Musicinator.Model.Impl
{
	public class ChainGang : IGang
	{
		private List<IGang> minionsEtc;
		private int current;
		private long duration;
		private List<long> startTimes;

		public ChainGang (params IGang[] minions)
		{
			this.minionsEtc = new List<IGang> (minions);
			this.duration = minions.Sum (m => m.Duration);
			this.current = 0;
			long total = 0;
			this.startTimes = minionsEtc.Select (x => {
				long result = total;
				total += x.Duration;
				return result;
			}).ToList ();
		}

		public long TimeToKill { get { return startTimes [this.current] + minionsEtc [this.current].TimeToKill; } }

		public long Duration { get { return this.duration; } }

		public IMinion GetSacrificialMinion ()
		{
			if (this.current == minionsEtc.Count)
				return null;
			IMinion result = minionsEtc [this.current].GetSacrificialMinion ();
			;
			if (minionsEtc [this.current].FizzledOut) {
				minionsEtc [this.current].Reset ();
				this.current++;
			}
			return result;
		}

		public bool FizzledOut {
			get {
				bool result = this.current == minionsEtc.Count;
				return result;
			}
		}

		public void Reset ()
		{
			this.current = 0;
		}
		
	}
}

