﻿using System;
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
			//Console.Write (tick + ":" + this.tick + "...");
			if (tick > this.tick) {
				Thread.Sleep (this.ts.MSPerTick * (int)(tick - this.tick));
			}
			if (this.tick != tick)
				Console.WriteLine ("Tick Error: " + (this.tick - tick));
		}

	}

	public class TimeSignature
	{
		[Obsolete]
		public static int TICKS_PER_QUARTER_NOTE = 24;
		public static int TICKS_PER_NOTE = 96;

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

		public static long GetTicksForNote (int noteValue)
		{
			return TimeSignature.TICKS_PER_NOTE / noteValue;
		}

		/// <summary>
		/// Gets the ticks for the number of notes.
		/// </summary>
		/// <returns>The ticks for notes.</returns>
		/// <param name="notes">Notes.</param>
		public static long GetTicksForNotes (int notes)
		{
			return TimeSignature.TICKS_PER_NOTE * notes;
		}

		/// <summary>
		/// How many whole notes for this number of bars.
		/// </summary>
		/// <returns>The number of notes.</returns>
		/// <param name="bars">the number of bars.</param>
		public int GetNoteForBars (int bars)
		{
			return bars * this.BeatsPerWholeNote / this.BeatsPerBar;
		}
	}
}

