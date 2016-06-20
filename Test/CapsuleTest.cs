using System;
using NUnit.Framework;
using Maestro.Minion;
using Maestro.Capsule;

namespace Test
{
	public class CapsuleTest
	{
		[Test]
		public void SortedCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 new SmallCapsule (12, new Note (Note.Pitch.B4, 8, 100)),
				                 new SmallCapsule (0, new Note (Note.Pitch.A4, 4, 85)));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (24, onNote1.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			Note onNote2 = notes.GetReadiedMinion (12) as Note;
			Assert.AreEqual (Note.Pitch.B4, onNote2.NotePitch);
			Assert.IsTrue (notes.CanDispose ());
		}

		[Test]
		public void SortedBigCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 Note.GetNote (12, Note.Pitch.B4, 8, 85),
				                 Note.GetNote (0, Note.Pitch.A4, 4, 85));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (Note.Pitch.A4, onNote1.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			Note onNote2 = notes.GetReadiedMinion (12) as Note;
			Assert.AreEqual (Note.Pitch.B4, onNote2.NotePitch);
			Assert.IsTrue (notes.CanDispose ());
		}


		[Test]
		public void TwoSmallCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 new SmallCapsule (0, new Note (Note.Pitch.A4, 4, 85)),
				                 new SmallCapsule (12, new Note (Note.Pitch.B4, 8, 100)));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (24, onNote1.TicksToComplete ());
			Assert.IsFalse (notes.CanDispose ());
			Note onNote2 = notes.GetReadiedMinion (12) as Note;
			Assert.AreEqual (Note.Pitch.B4, onNote2.NotePitch);
			Assert.IsTrue (notes.CanDispose ());
		}

		[Test]
		public void TwoSmallCollisionCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 new SmallCapsule (0, new Note (Note.Pitch.A4, 4, 85)),
				                 new SmallCapsule (0, new Note (Note.Pitch.B4, 8, 100)));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.IsFalse (notes.CanDispose ());
			Assert.IsNotNull (onNote1);
			Note onNote2 = notes.GetReadiedMinion (0) as Note;
			Assert.IsNotNull (onNote2);
			Assert.AreNotEqual (onNote1, onNote2);
			Assert.IsTrue (notes.CanDispose ());
		}


		[Test]
		public void TwoBigOverlapCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 Note.GetNote (0, Note.Pitch.A4, 4, 85),
				                 Note.GetNote (12, Note.Pitch.B4, 4, 85));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (Note.Pitch.A4, onNote1.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			Note onNote2 = notes.GetReadiedMinion (12) as Note;
			Assert.AreEqual (Note.Pitch.B4, onNote2.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			OffNote offNote1 = notes.GetReadiedMinion (24) as OffNote;
			Assert.AreEqual (Note.Pitch.A4, offNote1.NotePitch);
			OffNote offNote2 = notes.GetReadiedMinion (36) as OffNote;
			Assert.AreEqual (Note.Pitch.B4, offNote2.NotePitch);
			Assert.IsTrue (notes.CanDispose ());
		}

		[Test]
		public void SmallAndBigOverlapCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 new SmallCapsule (0, new Note (Note.Pitch.A4, 4, 85)),
				                 Note.GetNote (12, Note.Pitch.B4, 4, 85));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (Note.Pitch.A4, onNote1.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			Note onNote2 = notes.GetReadiedMinion (12) as Note;
			Assert.AreEqual (Note.Pitch.B4, onNote2.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			//OffNote offNote1 = notes.GetReadiedMinion (24) as OffNote;
			//Assert.AreEqual (Note.Pitch.A4, offNote1.NotePitch);
			OffNote offNote2 = notes.GetReadiedMinion (36) as OffNote;
			Assert.AreEqual (Note.Pitch.B4, offNote2.NotePitch);
			Assert.IsTrue (notes.CanDispose ());
		}

		[Test]
		public void BigAndSmallOverlapCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 Note.GetNote (0, Note.Pitch.A4, 4, 85),
				                 new SmallCapsule (12, new Note (Note.Pitch.B4, 4, 85)));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (Note.Pitch.A4, onNote1.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			Note onNote2 = notes.GetReadiedMinion (12) as Note;
			Assert.AreEqual (Note.Pitch.B4, onNote2.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			OffNote offNote1 = notes.GetReadiedMinion (24) as OffNote;
			Assert.AreEqual (Note.Pitch.A4, offNote1.NotePitch);
			Assert.IsTrue (notes.CanDispose ());
		}


		[Test]
		public void BigAndSmallApartCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 Note.GetNote (0, Note.Pitch.A4, 4, 85),
				                 new SmallCapsule (25, new Note (Note.Pitch.B4, 4, 85)));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (Note.Pitch.A4, onNote1.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			OffNote offNote1 = notes.GetReadiedMinion (24) as OffNote;
			Assert.IsFalse (notes.CanDispose ());
			Assert.AreEqual (Note.Pitch.A4, offNote1.NotePitch);
			Note onNote2 = notes.GetReadiedMinion (25) as Note;
			Assert.AreEqual (Note.Pitch.B4, onNote2.NotePitch);
			Assert.IsTrue (notes.CanDispose ());
		}

		[Test]
		public void BigAndSmallCollideCapsuleTest ()
		{
			ICapsule notes = new BigCapsule (0, 
				                 Note.GetNote (0, Note.Pitch.A4, 4, 85),
				                 new SmallCapsule (24, new Note (Note.Pitch.B4, 4, 85)));
			Note onNote1 = notes.GetReadiedMinion (0) as Note;
			Assert.AreEqual (Note.Pitch.A4, onNote1.NotePitch);
			Assert.IsFalse (notes.CanDispose ());
			IMinion m1 = notes.GetReadiedMinion (24);
			Assert.IsNotNull (m1);
			Assert.IsFalse (notes.CanDispose ());
			IMinion m2 = notes.GetReadiedMinion (24);
			Assert.IsNotNull (m2);
			Assert.AreNotEqual (m1, m2);
			Assert.IsTrue (notes.CanDispose ());
		}


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

