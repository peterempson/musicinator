using System;
using System.Collections.Generic;
using System.Linq;
using Maestro.Minion;
using Maestro.Control;
using System.Threading;

namespace Maestro.Capsule
{
	public class BigCapsule: ICapsule
	{

		private readonly ISet<ICapsule> capsules;
		private readonly long ticksToStart;
		private bool canDispose;

		public BigCapsule (long ticksToStart, params ICapsule[] capsules)
		{
			this.ticksToStart = ticksToStart;
			this.capsules = new SortedSet<ICapsule> (capsules, new Equalizer ());
			this.canDispose = false;
		}

		public IMinion GetReadiedMinion (long tick)
		{
			lock (this.capsules) {
				if (capsules.Count == 0)
					return null;
				ICapsule c = capsules.Min ();
				IMinion m = c.GetReadiedMinion (tick - this.ticksToStart);
				if (m != null) {
					if (c.CanDispose ()) {
						capsules.Remove (c);
					}
					this.canDispose |= capsules.Count == 0;
				}
				return m;
			}
		}

		public IMinion PeekAtNextMinion ()
		{
			if (capsules.Count == 0)
				return null;
			ICapsule c = capsules.GetFirst ();
			return c.PeekAtNextMinion ();
		}

		public bool CanDispose ()
		{
			return this.canDispose;
		}

		public long GetTicksToStart ()
		{
			if (capsules.Count == 0)
				return 0;
			return this.ticksToStart + this.capsules.GetFirst ().GetTicksToStart ();
		}

		public long GetTicksToComplete ()
		{
			long ticks = 0;
			this.capsules.ForEach (capsule => ticks += capsule.GetTicksToComplete ());
			return ticks;
		}


		public static ICapsule GetMarchingMinions (long ticksToStart, params ICapsule[] capsules)
		{
			if (capsules.Length == 0)
				throw new ArgumentException ("Can't march with no penguins");
			ICapsule[] newCapsules = new ICapsule[capsules.Length];
			long cumulatedTicksToStart = 0;
			for (int i = 0; i < capsules.Length; i++) {
				newCapsules [i] = new BigCapsule (cumulatedTicksToStart, capsules [i]);
				cumulatedTicksToStart += capsules [i].GetTicksToComplete ();
			}
			return new BigCapsule (ticksToStart, newCapsules);
		}

		public override String ToString ()
		{
			return String.Format ("BigCapsule: TicksToStart: {0} {1}", ticksToStart, capsules);
		}

	}
}

