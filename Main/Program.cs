using System;
using Maestro.Control;
using Maestro.Capsule;
using Maestro.Minion;

namespace Main
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			new MainClass ().doit ();
		}

		void doit ()
		{

			TickTock k = new TickTock ();
//			k.Add (Note.GetNote (Note.Pitch.A4, 4, 85));
//			k.Add (Note.GetNote (50, Note.Pitch.A4, 4, 85));

			ICapsule c = BigCapsule.GetMarchingMinions (0,
				             Note.GetNote (Note.Pitch.A4, 4, 85),
				             Note.GetNote (Note.Pitch.A4, 4, 100),
				             Note.GetNote (Note.Pitch.C5, 4, 85),
				             Note.GetNote (Note.Pitch.E5, 4, 85),
				             Note.GetNote (Note.Pitch.A5, 1, 100),
				             Note.GetNote (Note.Pitch.F5, 1, 100));
			k.Add (c);
	
			k.Start ();

			Console.ReadLine ();
		}
	}
}
