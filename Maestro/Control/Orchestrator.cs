using System;
using Maestro.Minion;

namespace Maestro.Control
{
	public interface Orchestrator
	{

		void Invoke (IMinion minion);

	}
}

