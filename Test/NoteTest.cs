using System;
using NUnit.Framework;
using Maestro.Minion;
using Maestro.Capsule;

namespace Test
{
	public class NoteTest
	{
		private ICapsule note;

		[SetUp]
		public void Setup ()
		{
			this.note = Note.GetNote (Note.Pitch.C5, 4, 100);
		}

		[Test]
		public void NoteOnAndOffTest ()
		{
			Assert.AreEqual (24, this.note.GetTicksToComplete (), "Ticks to complete");
			IMinion onNote = this.note.GetReadiedMinion (0);
			Assert.AreEqual (24, onNote.TicksToComplete ());
			Assert.IsFalse (this.note.CanDispose ());
			IMinion offNote = this.note.GetReadiedMinion (24);
			Assert.AreEqual (0, offNote.TicksToComplete ());
			Assert.IsTrue (this.note.CanDispose ());
		}
	}
}

