using System;
using Musicinator.Model;
using Musicinator.Model.Impl;
using Musicinator.Processor;
using Musicinator.Processor.Impl;

using Commons.Music.Midi;
using Commons.Music.Midi.PortMidi;

using System.Linq;

namespace Main
{
	public class App
	{
		private static IMidiOutput output;

		public static void Main (string[] args)
		{
			new App ().doit ();
		}


		public App ()
		{
			IMidiAccess access = new PortMidiAccess ();
			IMidiPortDetails mout = access.Outputs.ElementAt (1);
			output = access.OpenOutputAsync (mout).Result;
		}

		void doit ()
		{
			TimeSignature timeSig = new TimeSignature (4, 4, 120);
			TockTicker t = new TockTicker (timeSig);

			KickSequencer ks = new KickSequencer (timeSig);

			BassSequencer bs = new BassSequencer (timeSig);

			Scuttler b = new Scuttler (timeSig.GetNoteForBars (8), 
				             Enumerable.Range (0, 4).Select (_ => new League (
					             ks.GetKickSequence (1),
					             ks.GetSnareSequence (1), 
					             ks.GetHatSequence (1),
					             bs.GetBassSequence (1)
				             )).ToArray ());
			
			Scuttler c = new Scuttler (timeSig.GetNoteForBars (4), 
				             Enumerable.Range (0, 2).Select (_ => new League (
					             ks.GetKickSequence (1),
					             ks.GetSnareSequence (1), 
					             ks.GetHatSequence (1),
					             bs.GetBassSequence (1)
				             )).ToArray ());


			ChainGang cg5 = new ChainGang (b, c, b, c);


			IGang playThis = cg5;
			MidiMinionGrinder minionGrinder = new MidiMinionGrinder ();

			t.Start ();
			while (!playThis.FizzledOut) {
				t.SleepUntil (playThis.TimeToKill);
				do {
					IMinion m = playThis.GetSacrificialMinion ();
					if (!(m is NoOp))
						minionGrinder.Grind (m);
				} while (!playThis.FizzledOut && playThis.TimeToKill == 0);
				byte[] juices = minionGrinder.ExtractJuices ();
				output.SendAsync (juices, 0, juices.Length, 0);
			}

			//Console.ReadLine ();
		}
	}
}

