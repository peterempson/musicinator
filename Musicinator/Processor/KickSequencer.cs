using System;
using Musicinator.Model;
using Musicinator.Model.Impl;
using System.Linq;

namespace Musicinator.Processor
{
	public class KickSequencer
	{
		private readonly TimeSignature timeSig;
		private readonly Random rand;

		public KickSequencer (TimeSignature timeSig)
		{
			this.timeSig = timeSig;
			this.rand = new Random ();
		}

		public IGang GetKickSequence (int bars)
		{
			IGang[] result = new IGang[bars * timeSig.BeatsPerBar * 4];
			for (int i = 0; i < result.Length; i++) {
				result [i] = GetKick (i);
			}
			return new ChainGang (result);
		}

		public IGang GetSnareSequence (int bars)
		{
			IGang[] result = new IGang[bars * timeSig.BeatsPerBar * 4];
			for (int i = 0; i < result.Length; i++) {
				result [i] = GetSnare (i);
			}
			return new ChainGang (result);
		}

		public IGang GetHatSequence (int bars)
		{
			IGang[] result = new IGang[bars * timeSig.BeatsPerBar * 4];
			for (int i = 0; i < result.Length; i++) {
				result [i] = GetHat (i);
			}
			return new ChainGang (result);
		}

		private IGang GetKick (int ix)
		{
			int tx;
			if (ix % 8 == 0)
				tx = 18;
			else if (ix % 4 == 0)
				tx = 6;
			else if (ix % 2 == 0)
				tx = 2;
			else
				tx = 1;
			
			if (this.rand.Next (20) < tx)
				return new Drum (DrumPitch.AcousticBassDrum, 0.25 / 4, 100);
			else
				return new Rest (0.25 / 4);
		}

		private IGang GetSnare (int ix)
		{
			int tx;
			if (ix % 8 == 0)
				tx = 1;
			else if (ix % 4 == 0)
				tx = 18;
			else if (ix % 2 == 0)
				tx = 1;
			else
				tx = 1;

			if (this.rand.Next (20) < tx)
				return new Drum (DrumPitch.AcousticSnare, 0.25 / 4, 100);
			else
				return new Rest (0.25 / 4);
		}

		private IGang GetHat (int ix)
		{
			int tx;
			if (ix % 4 == 0)
				tx = 18;
			else if (ix % 2 == 0)
				tx = 8;
			else
				tx = 4;

			if (this.rand.Next (20) < tx) {
				if (this.rand.Next (10) == 0)
					return new Drum (DrumPitch.OpenHiHat, 0.25 / 4, 100);
				else
					return new Drum (DrumPitch.ClosedHiHat, 0.25 / 4, 100);
			} else
				return new Rest (0.25 / 4);
		}


	}
}

