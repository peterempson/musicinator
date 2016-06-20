using System;
using System.Threading;

namespace Musicinator.Processor
{
	public class TockTicker
	{
		private TimeSignature ts;
		private long tick;
		private Timer tickTimer;

		public TockTicker (TimeSignature ts)
		{
			this.tick = 0;
			this.ts = ts;
		}

		public void Start ()
		{
			this.tickTimer = new Timer (state => this.tick++, null, 100, ts.MSPerTick); 
		}

		public void Stop ()
		{
			this.tickTimer.Dispose ();
		}

		public void SleepUntil (long tick)
		{
			if (tick <= this.tick)
				return;
			Thread.Sleep (this.ts.MSPerTick * (int)(tick - this.tick));
		}

	}

	public class TimeSignature
	{
		public static int TICKS_PER_QUARTER_NOTE = 24;

		public TimeSignature (int BeatsPerBar, int BeatsPerWholeNote, int bpm)
		{
			this.BeatsPerBar = BeatsPerBar;
			this.BeatsPerWholeNote = BeatsPerWholeNote;
			this.BPM = bpm;
			this.UpdateMSPerTick ();
		}

		public  int BeatsPerBar { get; private set; }

		public  int BeatsPerWholeNote { get; private set; }

		public int BPM { get; private set; }

		public int MSPerTick { get; private set; }

		private void UpdateMSPerTick ()
		{
			int quarterNotesPerMinute = 4 * BPM / BeatsPerWholeNote;
			int ticksPerMinute = quarterNotesPerMinute * TICKS_PER_QUARTER_NOTE;
			this.MSPerTick = 60000 / ticksPerMinute;
		}

		public static long GetTicksForNote (int NoteValue)
		{
			return TimeSignature.TICKS_PER_QUARTER_NOTE * 4 / NoteValue;
		}
	}
}

