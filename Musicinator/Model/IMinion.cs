using System;

namespace Musicinator.Model
{
	public interface IMinion
	{
		/// <summary>
		/// Ticks until the start
		/// </summary>
		/// <value>The start.</value>
		long Start { get; }

		/// <summary>
		/// Ticks from start to finish
		/// </summary>
		/// <value>The duration.</value>
		long Duration { get; }

	}
}

