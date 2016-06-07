using System;
using Commons.Music.Midi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			new MainClass ().doit ();
		}

		async void doit() 
		{
			IMidiAccess access;

			access = MidiAccessManager.Default;
			IMidiPortDetails mout = access.Outputs.ElementAt(1);
			IMidiOutput output = access.OpenOutputAsync (mout).Result;
			Console.WriteLine (output.Connection);
			Task t = output.SendAsync (new byte[] { (byte) (0x91), 49, 100 }, 0, 3, 0);
			await t;

			//[0x90, 48, 100]
			//Console.WriteLine (access.Outputs.ElementAt(1));
		}
	}
}
