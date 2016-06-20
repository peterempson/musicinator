using System;

namespace Musicinator.Model
{
	public interface IGang: IMinion
	{
		IMinion GetSacrificialMinion ();

		/// <summary>
		/// Tests if there are no more minions.
		/// </summary>
		/// <returns><c>true</c>, if out was fizzleded, <c>false</c> otherwise.</returns>
		bool FizzledOut ();

	}
}

