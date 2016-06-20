using System;
using Maestro.Capsule;
using Maestro.Minion;
using Maestro.Control;
using System.Collections.Generic;
using System.Collections;

namespace Maestro.Control
{
	public class Equalizer<T>: IComparer
	{
		private readonly Dictionary<Type, int> typePriority;

		public Equalizer ()
		{
			this.typePriority = new Dictionary<Type, int> {
				{ typeof(ProgramChange), 1 },
				{ typeof(OffNote), 2 },
				{ typeof(Note), 3 }
			};
		}

		int IComparer.Compare (object x, object y)
		{
			return Equalizer.Compare (x as ICapsule, y as ICapsule);
		}

		public static int Compare (ICapsule a, ICapsule b)
		{
			if (a == b)
				return 0;
			if (a == null)
				return -1;
			if (b == null)
				return 1;
			int t = a.GetTicksToStart ().CompareTo (b.GetTicksToStart ());
			if (t != 0)
				return t;
			return Equalizer.Compare (a.PeekAtNextMinion (), b.PeekAtNextMinion ());
		}

		/// <summary>
		/// Compare Minions. Assumes they are not null
		/// </summary>
		/// <param name="">.</param>
		private static int Compare (IMinion a, IMinion b)
		{
			int pa = Equalizer.TypePriority [a.GetType ()];
			int pb = Equalizer.TypePriority [b.GetType ()];

			int d = pa.CompareTo (pb);
			if (d != 0)
				return d;
			return a.GetHashCode ().CompareTo (b.GetHashCode ());
		}
	}
}

