using System;

namespace Musicinator.Model
{
	public interface IGang
	{
		/// <summary>
		/// Ticks until the next thing
		/// </summary>
		/// <value>The start.</value>
		long TimeToKill { get; }

		/// <summary>
		/// Ticks from start to finish
		/// </summary>
		/// <value>The duration.</value>
		long Duration { get; }


		IMinion GetSacrificialMinion ();

		/// <summary>
		/// Tests if there are no more minions.
		/// </summary>
		/// <returns><c>true</c>, if out was fizzleded, <c>false</c> otherwise.</returns>
		bool FizzledOut { get; }

		/// <summary>
		/// Reset this instance.
		/// </summary>
		void Reset ();
	}
}

