using System;
using System.Collections.Generic;

namespace Musicinator.Model.Impl
{
	public abstract class Gang: IGang
	{
		protected List<IGang> minionsEtc;
		protected int current;
		protected long timeKilled;

		protected Gang (params IGang[] minions)
		{
			this.minionsEtc = new List<IGang> (minions);
			this.current = 0;
			this.timeKilled = 0;
		}

		public long TimeToKill { get { return this.timeKilled + minionsEtc [this.current].TimeToKill; } }

		public abstract long Duration { get; }

		public abstract IMinion GetSacrificialMinion ();

		public abstract bool FizzledOut { get; }

		public void Reset ()
		{
			this.timeKilled = 0;
			this.current = 0;
		}


	}
}

