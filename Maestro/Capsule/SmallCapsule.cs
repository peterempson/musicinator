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

		public IMinion PeekAtNextMinion ()
		{
			return this.Minion;
		}

		public bool CanDispose ()
		{
			return this.canDispose;
		}

		public long GetTicksToStart ()
		{
			return ticksToStart;
		}

		public long GetTicksToComplete ()
		{
			return Minion.TicksToComplete ();
		}

		public override String ToString ()
		{
			return String.Format ("SmallCapsule: TicksToStart: {0} {1}", ticksToStart, Minion);
		}
	}
}

