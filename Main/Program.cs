//using System;
//using Maestro.Control;
//using Maestro.Capsule;
//using Maestro.Minion;
//
//namespace Main
//{
//	class MainClass
//	{
//		public static void Main (string[] args)
//		{
//			new MainClass ().doit ();
//		}
//
//		void doit ()
//		{
//
//			TickTock k = new TickTock (new TaskMaster (), 140);
//			k.Add (new SmallCapsule (0, new ProgramChange (5)));
////			k.Add (new SmallCapsule (0, new Note (Note.Pitch.C6, 4, 45)));
////			k.Add (new SmallCapsule (0, new Note (Note.Pitch.E6, 4, 45)));
//			ICapsule c = BigCapsule.GetMarchingMinions (47,
//				             Note.GetNote (Note.Pitch.A4, 4, 85),
//				             Note.GetNote (Note.Pitch.A4, 4, 100),
//				             Note.GetNote (Note.Pitch.C5, 4, 85),
//				             Note.GetNote (Note.Pitch.E5, 4, 85),
//				             Note.GetNote (Note.Pitch.A5, 1, 100),
//				             Note.GetNote (Note.Pitch.F5, 1, 100));
////			k.Add (c);
//			k.Add (new BigCapsule (0, Note.GetNote (0, Note.Pitch.A3, 4, 85),
//				Note.GetNote (1, Note.Pitch.B3, 4, 85)
//			));
//			k.Add (new BigCapsule (24, Note.GetNote (0, Note.Pitch.C3, 4, 85),
//				Note.GetNote (25, Note.Pitch.D3, 4, 85)
//			));
//	
//			k.Start ();
//
//			Console.ReadLine ();
//		}
//	}
//}
