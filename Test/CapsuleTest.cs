using System;
using NUnit.Framework;
using Maestro.Minion;
using Maestro.Capsule;

namespace Test
{
	public class CapsuleTest
	{

		[Test]
		public void NestedCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 Note.GetNote (0, Note.Pitch.A4, 4, 85),
				                 Note.GetNote (24, Note.Pitch.B4, 8, 100));
			Assert.AreEqual (36, notes.GetTicksToComplete (), "Ticks to complete");
			IMinion onNote1 = notes.GetReadiedMinion (0);
			Assert.AreEqual (24, onNote1.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			IMinion offNote1 = notes.GetReadiedMinion (24);
			Assert.AreEqual (0, offNote1.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			Assert.AreEqual (12, notes.GetTicksToComplete (), "Ticks to complete");
			IMinion onNote2 = notes.GetReadiedMinion (24);
			Assert.AreEqual (12, onNote2.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			IMinion offNote2 = notes.GetReadiedMinion (36);
			Assert.AreEqual (0, offNote2.TicksToComplete ());
			Assert.IsTrue (notes.CanDispose ());

	
		}

		[Test]
		public void MarchingMinionsTest ()
		{
			ICapsule notes = BigCapsule.GetMarchingMinions (0,
				                 Note.GetNote (Note.Pitch.A4, 4, 85),
				                 Note.GetNote (Note.Pitch.B4, 8, 100));
			Assert.AreEqual (36, notes.GetTicksToComplete (), "Ticks to complete");
			IMinion onNote1 = notes.GetReadiedMinion (0);
			Assert.AreEqual (24, onNote1.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			IMinion offNote1 = notes.GetReadiedMinion (24);
			Assert.AreEqual (0, offNote1.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			Assert.AreEqual (12, notes.GetTicksToComplete (), "Ticks to complete");
			IMinion onNote2 = notes.GetReadiedMinion (24);
			Assert.AreEqual (12, onNote2.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			IMinion offNote2 = notes.GetReadiedMinion (36);
			Assert.AreEqual (0, offNote2.TicksToComplete ());
			Assert.IsTrue (notes.CanDispose ());

		}
	}
}

