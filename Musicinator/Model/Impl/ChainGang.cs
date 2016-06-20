using System;
using System.Collections.Generic;
using Musicinator.Model;

namespace Musicinator.Model.Impl
{
	//	public class ChainGang: IGang
	//	{
	//		public long Start { get { return ns.Start; } }
	//
	//		public long Duration { get { return ns.Duration; } }
	//
	//		private List<IMinion> minionsEtc;
	//		private int state;
	//
	//		public ChainGang (params IMinion[] minions)
	//		{
	//			this.minionsEtc = new List<IMinion> (minions);
	//			this.state = 1;
	//		}
	//
	//		public IMinion GetSacrificialMinion ()
	//		{
	//			IMinion result = null;
	//			switch (this.state) {
	//			case 1:
	//				result = this.ns;
	//				this.state++;
	//				break;
	//			case 2:
	//				result = this.ne;
	//				this.state++;
	//				break;
	//			}
	//			return result;
	//		}
	//
	//		public bool FizzledOut ()
	//		{
	//			bool result = this.state == 3;
	//			this.state = 1;
	//			return result;
	//		}
	//
	//	}
}

