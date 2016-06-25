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


			ChainGang cg1 = new ChainGang (
				                new Drum (DrumPitch.AcousticBassDrum, 4, 100), 
				                new Drum (DrumPitch.AcousticSnare, 4, 100),
				                new Drum (DrumPitch.AcousticBassDrum, 4, 100),
				                new Drum (DrumPitch.AcousticSnare, 4, 100)
			                );

			ChainGang cg2 = new ChainGang (
				                new Drum (DrumPitch.AcousticBassDrum, 8, 100), 
				                new Drum (DrumPitch.AcousticBassDrum, 8, 100), 
				                new Drum (DrumPitch.AcousticSnare, 8, 100),
				                new Drum (DrumPitch.AcousticBassDrum, 8, 100), 
				                new Drum (DrumPitch.AcousticBassDrum, 4, 100),
				                new Drum (DrumPitch.AcousticBassDrum, 8, 100), 
				                new Drum (DrumPitch.AcousticSnare, 8, 100)
			                );

			ChainGang cg3 = new ChainGang (
				                new Drum (DrumPitch.AcousticBassDrum, 2, 100), 
				                new Drum (DrumPitch.AcousticBassDrum, 4, 100),
				                new Drum (DrumPitch.AcousticSnare, 4, 100)
			                );

			ChainGang cg4 = new ChainGang (
				                new Drum (DrumPitch.AcousticBassDrum, 4, 100), 
				                new Drum (DrumPitch.AcousticSnare, 8, 100),
				                new Drum (DrumPitch.AcousticSnare, 8, 100),
				                new Drum (DrumPitch.AcousticBassDrum, 4, 100),
				                new Drum (DrumPitch.AcousticSnare, 4, 100)
			                );

			Scuttler b = new Scuttler (timeSig.GetNoteForBars (8), cg1, cg2, cg3, cg4);


			IGang playThis = b;
			MidiMinionGrinder minionGrinder = new MidiMinionGrinder (9);

			t.Start ();
			while (!playThis.FizzledOut) {
				t.SleepUntil (playThis.TimeToKill);
				do {
					IMinion m = playThis.GetSacrificialMinion ();
					minionGrinder.Grind (m);
				} while (false); // while (timetokill hasn't changed)
				byte[] juices = minionGrinder.ExtractJuices ();
				output.SendAsync (juices, 0, juices.Length, 0);
			}

			Console.ReadLine ();
		}
	}
}

