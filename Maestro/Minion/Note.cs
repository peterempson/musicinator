﻿using System;
using Maestro.Capsule;
using Maestro.Control;

namespace Maestro.Minion
{
	public class Note: IMidiMinion
	{
		public Pitch NotePitch { get; private set; }

		public int NoteValue { get; private set; }

		public int NoteVelocity { get; private set; }

		Note (Pitch pitch, int value, int velocity)
		{
			this.NotePitch = pitch;
			// test >0 && <= 64 && power of 2
			if (value < 1 || value > 64 || (value & (value - 1)) != 0)
				throw new ArgumentException ("Invalid note value. Note values must be a power of 2 between 1 and 64");
			this.NoteValue = value;
			if (velocity < 0 || velocity > 127)
				throw new ArgumentException ("Invalid velocity. Velocity must be between 0 and 127");
			this.NoteVelocity = velocity;
		}

		public byte[] ExtractJuices ()
		{
			return new byte[]{ (byte)MidiThings.Control.NoteOn, (byte)NotePitch, (byte)NoteVelocity };
		}

		public long TicksToComplete ()
		{
			return TimeSignature.GetTicksForNote (this.NoteValue);
		}

		public static ICapsule GetNote (long ticksToStart, Pitch pitch, int value, int velocity)
		{
			SmallCapsule non = new SmallCapsule (0, new Note (pitch, value, velocity));
			SmallCapsule noff = new SmallCapsule (TimeSignature.GetTicksForNote (value), new OffNote (pitch));
			return new BigCapsule (ticksToStart, non, noff);
		}

		public static ICapsule GetNote (Pitch pitch, int value, int velocity)
		{
			return GetNote (0, pitch, value, velocity);
		}

		public override String ToString ()
		{
			return String.Format ("Note: 1/{0} {1} @ {2}", NoteValue, NotePitch, NoteVelocity);
		}

		public enum Pitch
		{
			C0 = 0,
			Cs0 = 1,
			D0 = 2,
			Ds0 = 3,
			E0 = 4,
			F0 = 5,
			Fs0 = 6,
			G0 = 7,
			Gs0 = 8,
			A0 = 9,
			As0 = 10,
			B0 = 11,
			C1 = 12,
			Cs1 = 13,
			D1 = 14,
			Ds1 = 15,
			E1 = 16,
			F1 = 17,
			Fs1 = 18,
			G1 = 19,
			Gs1 = 20,
			A1 = 21,
			As1 = 22,
			B1 = 23,
			C2 = 24,
			Cs2 = 25,
			D2 = 26,
			Ds2 = 27,
			E2 = 28,
			F2 = 29,
			Fs2 = 30,
			G2 = 31,
			Gs2 = 32,
			A2 = 33,
			As2 = 34,
			B2 = 35,
			C3 = 36,
			Cs3 = 37,
			D3 = 38,
			Ds3 = 39,
			E3 = 40,
			F3 = 41,
			Fs3 = 42,
			G3 = 43,
			Gs3 = 44,
			A3 = 45,
			As3 = 46,
			B3 = 47,
			C4 = 48,
			Cs4 = 49,
			D4 = 50,
			Ds4 = 51,
			E4 = 52,
			F4 = 53,
			Fs4 = 54,
			G4 = 55,
			Gs4 = 56,
			A4 = 57,
			As4 = 58,
			B4 = 59,
			C5 = 60,
			Cs5 = 61,
			D5 = 62,
			Ds5 = 63,
			E5 = 64,
			F5 = 65,
			Fs5 = 66,
			G5 = 67,
			Gs5 = 68,
			A5 = 69,
			As5 = 70,
			B5 = 71,
			C6 = 72,
			Cs6 = 73,
			D6 = 74,
			Ds6 = 75,
			E6 = 76,
			F6 = 77,
			Fs6 = 78,
			G6 = 79,
			Gs6 = 80,
			A6 = 81,
			As6 = 82,
			B6 = 83,
			C7 = 84,
			Cs7 = 85,
			D7 = 86,
			Ds7 = 87,
			E7 = 88,
			F7 = 89,
			Fs7 = 90,
			G7 = 91,
			Gs7 = 92,
			A7 = 93,
			As7 = 94,
			B7 = 95,
			C8 = 96,
			Cs8 = 97,
			D8 = 98,
			Ds8 = 99,
			E8 = 100,
			F8 = 101,
			Fs8 = 102,
			G8 = 103,
			Gs8 = 104,
			A8 = 105,
			As8 = 106,
			B8 = 107,
			C9 = 108,
			Cs9 = 109,
			D9 = 110,
			Ds9 = 111,
			E9 = 112,
			F9 = 113,
			Fs9 = 114,
			G9 = 115,
			Gs9 = 116,
			A9 = 117,
			As9 = 118,
			B9 = 119,
			C10 = 120,
			Cs10 = 121,
			D10 = 122,
			Ds10 = 123,
			E10 = 124,
			F10 = 125,
			Fs10 = 126,
			G10 = 127
		}

	}
}

