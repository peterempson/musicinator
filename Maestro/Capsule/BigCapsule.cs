using System;
using Wintellect.PowerCollections;
using Maestro.Minion;

namespace Maestro.Capsule
{
	public class BigCapsule: ICapsule
	{
		private readonly OrderedSet<ICapsule> capsules;
		private readonly long ticksToStart;

		public BigCapsule (long ticksToStart, params ICapsule[] capsules)
		{
			this.ticksToStart = ticksToStart;
			this.capsules = new OrderedSet<ICapsule> (capsules);
		}

		IMinion ICapsule.GetReadiedMinion (long tick)
		{
			if (capsules.Count == 0)
				return null;
			ICapsule c = capsules.GetFirst ();
			IMinion m = c.GetReadiedMinion (tick - this.ticksToStart);
			if (c.CanDispose ()) {
				capsules.RemoveFirst ();
			}
			return m;
		}

		bool ICapsule.CanDispose ()
		{
			return capsules.Count == 0;
		}

		public long GetTicksToStart ()
		{
			return this.ticksToStart + this.capsules.GetFirst ().GetTicksToStart ();
		}

		long ICapsule.GetTicksToComplete ()
		{
			long ticks = 0;
			this.capsules.ForEach (capsule => ticks += capsule.GetTicksToComplete ());
			return ticks;
		}

		int IComparable.CompareTo (object obj)
		{
			if (obj == null)
				return 1;

			ICapsule otherCapsule = obj as ICapsule;
			if (otherCapsule != null)
				return this.GetTicksToStart ().CompareTo (otherCapsule.GetTicksToStart ());
			else
				throw new ArgumentException ("Object is not an ICapsule");
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

