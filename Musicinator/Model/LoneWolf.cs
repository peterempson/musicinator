using System;
using Musicinator.Processor;

namespace Musicinator.Model
{
	public abstract class LoneWolf: IGang, IMinion
	{
		public LoneWolf ()
		{
			this.FizzledOut = false;
		}

		public long Duration { get { return 0; } }

		public long TimeToKill { get { return 0; } }

		public bool FizzledOut { get; private set; }

		public IMinion GetSacrificialMinion ()
		{
			this.FizzledOut = true;
			return this;
		}

		public void Reset ()
		{
			this.FizzledOut = false;
		}

	}

}

