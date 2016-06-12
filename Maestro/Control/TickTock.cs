using System;
using System.Threading;
using Wintellect.PowerCollections;
using Maestro.Capsule;
using Maestro.Minion;

namespace Maestro.Control
{
	public class TickTock
	{

		private static TimeSignature ts = new TimeSignature (4, 4);
		private long tick;
		private readonly OrderedSet<ICapsule> capsules;
		private TaskMaster taskMaster;

		public TickTock ()
		{
			taskMaster = new TaskMaster ();
			capsules = new OrderedSet<ICapsule> ();
		}

		public void Start ()
		{
			if (capsules.Count == 0) {
				throw new NotSupportedException ("Not starting as no capsules have been added");
			}
			new Timer (this.Tock, null, 100, ts.GetMSPerTick (120)); 
		}

		private void Tock (Object state)
		{
			
			if (capsules.Count > 0) {
				ICapsule c = capsules.GetFirst ();
				IMinion m = c.GetReadiedMinion (tick);
				while (m != null) {
					Console.Write (tick + " ");
					if (c.CanDispose ())
						capsules.RemoveFirst ();
					taskMaster.Invoke (m);
					m = c.GetReadiedMinion (tick);
				}
			}
			tick++;
		}

		public void Add (ICapsule c)
		{
			capsules.Add (c);
		}


		public bool IsFinished ()
		{
			return capsules.Count == 0;
		}
		
	}
}

