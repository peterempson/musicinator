using System;
using Maestro.Minion;

namespace Maestro.Capsule
{
	public interface ICapsule: IComparable
	{
		IMinion GetReadiedMinion (long tick);

		bool CanDispose ();

		long GetTicksToStart ();

		long GetTicksToComplete ();

	}
}

