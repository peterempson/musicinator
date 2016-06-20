using System;
using Musicinator.Processor;

namespace Musicinator.Model.Impl
{
	public class Note: IGang
	{
		public long Start {
			get {
				if (state == 1)
					return ns.Start;
				else if (state == 2)
					return ne.Start;
				return 0;
			}
		}

		public long Duration { get { return ns.Duration; } }

		private NoteStart ns;
		private NoteEnd ne;

		/// <summary>
		/// 1 - ready to play noteStart
		/// 2 - ready to play notEnd
		/// 0 - fizzled out
		/// </summary>
		private int state;

		public Note (long start, Pitch notePitch, int noteValue, int velocity)
		{
			this.ns = new NoteStart (start, notePitch, noteValue, velocity);
			this.ne = new NoteEnd (start + TimeSignature.GetTicksForNote (noteValue), notePitch);
			this.state = 1;
		}

		public IMinion GetSacrificialMinion ()
		{
			IMinion result = null;
			switch (this.state) {
			case 1:
				result = this.ns;
				this.state++;
				break;
			case 2:
				result = this.ne;
				this.state = 0;
				break;
			}
			return result;
		}

		public bool FizzledOut ()
		{
			bool result = this.state == 0;
			this.state++;
			return result;
		}

	}

	public class NoteEnd: IMinion
	{
		public long Start { get; private set; }

		public virtual long Duration { get { return 0; } }

		public Pitch NotePitch { get; private set; }

		public NoteEnd (long start, Pitch notePitch)
		{
			this.Start = start;
			this.NotePitch = notePitch;
		}


	}

	public class NoteStart: NoteEnd
	{

		public override long Duration { get { return TimeSignature.GetTicksForNote (noteValue); } }

		public int Velocity { get; private set; }

		private readonly int noteValue;

		public NoteStart (long start, Pitch notePitch, int noteValue, int velocity) : base (start, notePitch)
		{
			this.noteValue = noteValue;
			this.Velocity = velocity;
		}

	}


	public enum Pitch
	{
		C0,
		Cs0,
		D0,
		Ds0,
		E0,
		F0,
		Fs0,
		G0,
		Gs0,
		A0,
		As0,
		B0,
		C1,
		Cs1,
		D1,
		Ds1,
		E1,
		F1,
		Fs1,
		G1,
		Gs1,
		A1,
		As1,
		B1,
		C2,
		Cs2,
		D2,
		Ds2,
		E2,
		F2,
		Fs2,
		G2,
		Gs2,
		A2,
		As2,
		B2,
		C3,
		Cs3,
		D3,
		Ds3,
		E3,
		F3,
		Fs3,
		G3,
		Gs3,
		A3,
		As3,
		B3,
		C4,
		Cs4,
		D4,
		Ds4,
		E4,
		F4,
		Fs4,
		G4,
		Gs4,
		A4,
		As4,
		B4,
		C5,
		Cs5,
		D5,
		Ds5,
		E5,
		F5,
		Fs5,
		G5,
		Gs5,
		A5,
		As5,
		B5,
		C6,
		Cs6,
		D6,
		Ds6,
		E6,
		F6,
		Fs6,
		G6,
		Gs6,
		A6,
		As6,
		B6,
		C7,
		Cs7,
		D7,
		Ds7,
		E7,
		F7,
		Fs7,
		G7,
		Gs7,
		A7,
		As7,
		B7,
		C8,
		Cs8,
		D8,
		Ds8,
		E8,
		F8,
		Fs8,
		G8,
		Gs8,
		A8,
		As8,
		B8,
		C9,
		Cs9,
		D9,
		Ds9,
		E9,
		F9,
		Fs9,
		G9,
		Gs9,
		A9,
		As9,
		B9,
		C10,
		Cs10,
		D10,
		Ds10,
		E10,
		F10,
		Fs10,
		G10
	}

}

