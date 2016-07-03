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

		protected override void UpdateCurrent ()
		{
			this.current++;
		}

		public override bool FizzledOut {
			get {
				return this.current == minionsEtc.Count;
			}
		}
	}
}

