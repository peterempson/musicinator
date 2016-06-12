using System;
using Maestro.Minion;


using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace Maestro.Capsule
{
	public class SmallCapsule: ICapsule
	{
		private long ticksToStart;

		private IMinion Minion { get; set; }

		private bool canDispose;

		public SmallCapsule (long ticksToStart, IMinion minion)
		{
			this.ticksToStart = ticksToStart;
			this.Minion = minion;
			this.canDispose = false;
		}

		public IMinion GetReadiedMinion (long tick)
		{
			if (tick == ticksToStart) {
				this.canDispose = true;
				return Minion;
			}
			return null;
		}

		public bool CanDispose ()
		{
			return this.canDispose;
		}

		long ICapsule.GetTicksToStart ()
		{
			return ticksToStart;
		}

		long ICapsule.GetTicksToComplete ()
		{
			return Minion.TicksToComplete ();
		}

		public int CompareTo (object obj)
		{
			if (obj == null)
				return 1;

			SmallCapsule otherCapsule = obj as SmallCapsule;
			if (otherCapsule != null)
				return this.ticksToStart.CompareTo (otherCapsule.ticksToStart);
			else
				throw new ArgumentException ("Object is not a Capsule");
		}

		public override String ToString ()
		{
			return String.Format ("SmallCapsule: TicksToStart: {0} {1}", ticksToStart, Minion);
		}
	}
}

