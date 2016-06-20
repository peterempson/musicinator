using System;
using Maestro.Minion;

namespace Maestro.Capsule
{
	public interface ICapsule
	{
		IMinion GetReadiedMinion (long tick);

		bool CanDispose ();

		long GetTicksToStart ();

		long GetTicksToComplete ();

		IMinion PeekAtNextMinion ();
	}
}

