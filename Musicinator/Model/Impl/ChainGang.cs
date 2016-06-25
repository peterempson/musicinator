using System;
using System.Collections.Generic;
using Musicinator.Model;
using System.Linq;

namespace Musicinator.Model.Impl
{
	public class ChainGang : Gang
	{

		private long duration;

		public ChainGang (params IGang[] minions) : base (minions)
		{
			this.duration = minions.Sum (m => m.Duration);
		}

		public override long Duration { get { return this.duration; } }

		public override IMinion GetSacrificialMinion ()
		{
			if (this.current == minionsEtc.Count)
				return null;
			IMinion result = minionsEtc [this.current].GetSacrificialMinion ();
			if (minionsEtc [this.current].FizzledOut) {
				this.timeKilled += minionsEtc [this.current].Duration;

				minionsEtc [this.current].Reset ();
				this.current++;
			}
			return result;
		}

		public override bool FizzledOut {
			get {
				return this.current == minionsEtc.Count;
			}
		}
	}
}

