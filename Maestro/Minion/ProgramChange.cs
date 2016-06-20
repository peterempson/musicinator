using System;
using Maestro.Control;

namespace Maestro.Minion
{
	public class ProgramChange: IMidiMinion
	{
		private readonly int program;

		public ProgramChange (int program)
		{
			this.program = program;
		}

		long IMinion.TicksToComplete ()
		{
			return 0;
		}

		byte[] IMidiMinion.ExtractJuices (int channel)
		{
			if (channel < 0 || channel > 15)
				throw new ArgumentException ("Channel is between 0 and 15");
			return new byte[]{ (byte)(MidiThings.Control.Program + channel), (byte)program, 0 };
		}
		
	}
}

