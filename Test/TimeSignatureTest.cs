using System;
using NUnit.Framework;
using Maestro.Control;

namespace Test
{
	public class TimeSignatureTest
	{
		[Test]
		public void MsPerTickTest ()
		{
			var ts = new TimeSignature (3, 4);

			Assert.AreEqual (25, ts.GetMSPerTick (100), "ms per tick");

		}

		[Test]
		public void GetTicksForNoteTest ()
		{
			Assert.AreEqual (24, TimeSignature.GetTicksForNote (4));
		}
	}
}

