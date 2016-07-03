using System;
using Musicinator.Model;
using Musicinator.Model.Impl;
using System.Linq;

namespace Musicinator.Processor
{
	public class BassSequencer
	{
		private readonly TimeSignature timeSig;
		private readonly Random rand;
		private readonly Pitch[] p = { Pitch.C2, Pitch.G2, Pitch.E2 };


		public BassSequencer (TimeSignature timeSig)
		{
			this.timeSig = timeSig;
			this.rand = new Random ();
		}

		public IGang GetBassSequence (int bars)
		{
			IGang[] result = new IGang[bars * timeSig.BeatsPerBar + 1];
			result [0] = new InstrumentChange (Instrument.ElectricBassFinger);
			for (int i = 1; i < result.Length; i++) {
				result [i] = new Note (p [rand.Next (3)], .25, 100);
			}
			return new ChainGang (result);
		}


	}
}

