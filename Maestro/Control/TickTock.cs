using System;
using System.Threading;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using Maestro.Capsule;
using Maestro.Minion;
using System.Threading;


namespace Maestro.Control
{
	public class TickTock
	{

		private static TimeSignature ts = new TimeSignature (4, 4);
		private long tick;
		private readonly OrderedSet<ICapsule> capsules;
		private TaskMaster taskMaster;

		public int BPM { get; private set; }
		//TODO: variable BPM.

		public TickTock (TaskMaster taskMaster, int bpm)
		{
			this.taskMaster = taskMaster;
			capsules = new OrderedSet<ICapsule> (Equalizer.Compare);
			this.BPM = bpm;
			this.tick = 0;
		}

		public void Start ()
		{
			if (capsules.Count == 0) {
				throw new NotSupportedException ("Not starting as no capsules have been added");
			}
			new Timer (this.Tock, null, 100, ts.GetMSPerTick (BPM)); 
		}

		private void Tock (Object state)
		{
			if (tick == 24) {
				Console.WriteLine ("{0} *** {1}", tick, capsules);
				
			}
			List<IMinion> toDo = new List<IMinion> ();
			lock (this.capsules) {
				long t = tick++;
				if (capsules.Count == 0)
					return;
				ICapsule c = capsules.GetFirst ();
				IMinion m = c.GetReadiedMinion (t);
				while (m != null) {
					Console.WriteLine ("{0}\t Playing: {1}", t, m);
					capsules.Remove (c);
					if (t == 24) {
						Console.WriteLine ("----- Popped: {0} {1}", c.GetTicksToStart (), capsules);

					}
					if (!c.CanDispose ()) {
						// Sorted to the new start tick
						capsules.Add (c);
					}

					if (t == 24) {
						Console.WriteLine ("------ Reorg: {0} {1}", c.GetTicksToStart (), capsules);

					}
					toDo.Add (m);
					if (capsules.Count == 0)
						break;
					c = capsules.GetFirst ();
					m = c.GetReadiedMinion (t);
				}
			}
			//taskMaster.Invoke (toDo);
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

