using System;
using Musicinator.Processor;

namespace Musicinator.Model.Impl
{

	public abstract class Deuce: IGang
	{
		private IMinion ns;
		private IMinion ne;
		private double noteLength;
		private int state;

		protected Deuce (IMinion ns, IMinion ne, double noteLength)
		{
			this.ns = ns;
			this.ne = ne;
			this.noteLength = noteLength;
			this.state = 1;
		}

		public long Duration { get { return TimeSignature.GetTicksForNote (this.noteLength); } }

		public long TimeToKill {
			get {
				if (state == 2)
					return TimeSignature.GetTicksForNote (this.noteLength);
				return 0;
			}
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

		public bool FizzledOut {
			get {
				bool result = this.state == 0;

				return result;
			}
		}

		public void Reset ()
		{
			this.state = 1;
		}

	}

	public class Note: Deuce
	{
		public Note (Pitch notePitch, double noteLength, int velocity) :
			base (new NoteStart (notePitch, velocity), new NoteEnd (notePitch), noteLength)
		{
		}
	}

	public class Drum: Deuce
	{
		public Drum (DrumPitch d, double noteLength, int velocity) :
			base (new DrumStart (d, velocity), new DrumEnd (d), noteLength)
		{
		}
		
	}

	public class Rest: Deuce
	{
		public Rest (double noteLength) :
			base (new NoOp (), new NoOp (), noteLength)
		{
		}

	}


	public class NoteEnd: IMinion
	{
		public NoteEnd (Pitch notePitch)
		{
			this.NotePitch = notePitch;
		}

		public Pitch NotePitch { get; private set; }

	}

	public class NoteStart: NoteEnd
	{
		public NoteStart (Pitch notePitch, int velocity) : base (notePitch)
		{
			this.Velocity = velocity;
		}

		public int Velocity { get; private set; }

	}

	public class DrumEnd: IMinion
	{
		public DrumEnd (DrumPitch drumPitch)
		{
			this.DrumPitch = drumPitch;
		}

		public DrumPitch DrumPitch { get; private set; }

	}

	public class DrumStart: DrumEnd
	{
		public DrumStart (DrumPitch drumPitch, int velocity) : base (drumPitch)
		{
			this.Velocity = velocity;
		}

		public int Velocity { get; private set; }

	}

	public class NoOp: IMinion
	{
		public NoOp ()
		{
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

	public enum DrumPitch
	{
		AcousticBassDrum,
		BassDrum1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbalHighTom,
		RideCymbalChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbalVibraslap,
		RideCymbalHiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle
	}

}

