using System;
using Maestro.Minion;

namespace Maestro.Control
{
	public class TaskMaster
	{
		private MidiOrchestrator midiOrchestrator;

		public TaskMaster ()
		{
			this.midiOrchestrator = new MidiOrchestrator ();
		}

		public void Invoke (IMinion minion)
		{
			if (minion is IMidiMinion)
				this.midiOrchestrator.Invoke ((IMidiMinion)minion);
			else
				throw new Exception ("Bad minion");
		}
	}
}

