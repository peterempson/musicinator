using System;
using Commons.Music.Midi;
using Commons.Music.Midi.PortMidi;
using System.Linq;
using Maestro.Minion;

namespace Maestro.Control
{
	public class MidiOrchestrator: Orchestrator
	{
		private static IMidiOutput output;

		static MidiOrchestrator ()
		{
			IMidiAccess access = new PortMidiAccess ();
			IMidiPortDetails mout = access.Outputs.ElementAt (1);
			output = access.OpenOutputAsync (mout).Result;
		}

		public MidiOrchestrator ()
		{
		}

		public void Invoke (IMinion minion)
		{
			IMidiMinion m = minion as IMidiMinion;
			if (m != null) {
				byte[] juices = m.ExtractJuices (0);
				output.SendAsync (juices, 0, 3, 0);
			}

		}

	}

}

