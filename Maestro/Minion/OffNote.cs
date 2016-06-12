using System;
using Maestro.Control;

namespace Maestro.Minion
{
	public class OffNote: IMidiMinion
	{
		private Note.Pitch notePitch;

		public OffNote (Note.Pitch pitch)
		{
			this.notePitch = pitch;
		}

		public byte[] ExtractJuices ()
		{
			return new byte[]{ (byte)MidiThings.Control.NoteOff, (byte)notePitch, 0 };
		}


		long IMinion.TicksToComplete ()
		{
			return 0;
		}

	}
}

