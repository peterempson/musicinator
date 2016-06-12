using System;

namespace Maestro.Control
{
	public class TimeSignature
	{
		public static int TICKS_PER_QUARTER_NOTE = 24;

		public TimeSignature (int BeatsPerBar, int BeatsPerWholeNote)
		{
			this.BeatsPerBar = BeatsPerBar;
			this.BeatsPerWholeNote = BeatsPerWholeNote;
		}

		public  int BeatsPerBar { get; private set; }

		public  int BeatsPerWholeNote { get; private set; }

		public long GetMSPerTick (int BPM)
		{
			int quarterNotesPerMinute = 4 * BPM / BeatsPerWholeNote;
			long ticksPerMinute = quarterNotesPerMinute * TICKS_PER_QUARTER_NOTE;
			return 60000 / ticksPerMinute;
		}

		public static long GetTicksForNote (int NoteValue)
		{
			return TimeSignature.TICKS_PER_QUARTER_NOTE * 4 / NoteValue;
		}
	}
}

