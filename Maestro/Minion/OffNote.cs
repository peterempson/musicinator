using System;
using Maestro.Control;

namespace Maestro.Minion
{
	public class OffNote: IMidiMinion
	{
		public Note.Pitch NotePitch { get; private set; }

		public OffNote (Note.Pitch pitch)
		{
			this.NotePitch = pitch;
		}

		public byte[] ExtractJuices (int channel)
		{
			if (channel < 0 || channel > 15)
				throw new ArgumentException ("Channel is between 0 and 15");
			return new byte[]{ (byte)(MidiThings.Control.NoteOff + channel), (byte)NotePitch, 0 };
		}


		long IMinion.TicksToComplete ()
		{
			return 0;
		}

		public override String ToString ()
		{
			return String.Format ("Off Note: {0}", NotePitch);
		}


	}
}

