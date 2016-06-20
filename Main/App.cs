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

			TockTicker t = new TockTicker (new TimeSignature (4, 4, 120));


			Note note = new Note (0, Pitch.C4, 4, 100);
			MidiMinionGrinder minionGrinder = new MidiMinionGrinder ();
			minionGrinder.Grind (note.GetSacrificialMinion ());

			t.Start ();

			byte[] juices = minionGrinder.ExtractJuices ();
			output.SendAsync (juices, 0, juices.Length, 0);

			t.SleepUntil (note.Start);
			minionGrinder.Grind (note.GetSacrificialMinion ());
			juices = minionGrinder.ExtractJuices ();
			output.SendAsync (juices, 0, juices.Length, 0);

			Console.ReadLine ();
		}
	}
}

