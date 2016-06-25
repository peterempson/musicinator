using System;
using System.Collections.Generic;
using Musicinator.Model;
using Musicinator.Processor;
using System.Linq;

namespace Musicinator.Model.Impl
{
	/// <summary>
	/// So named as the bell shaped curve that determines the likelyhood of any minion being sacrificed
	/// is reminscent of bell bottom trowsers the like of which were worn by scuttlers; youth gangs 
	/// in late 19th Century Manchester. 
	/// </summary>
	public class Scuttler: Gang
	{
		private readonly Gaussianator gr;
		private long duration;

		public Scuttler (int notes, params IGang[] minions) : base (minions)
		{
			this.duration = TimeSignature.GetTicksForNotes (notes);
			this.gr = new Gaussianator (this.minionsEtc.Count - 1);
			this.current = gr.GetGaussian ();
		}

		public override long Duration { get { return this.duration; } }

		public override IMinion GetSacrificialMinion ()
		{
			IMinion result = minionsEtc [this.current].GetSacrificialMinion ();
			if (minionsEtc [this.current].FizzledOut) {
				this.timeKilled += minionsEtc [this.current].Duration;
				this.current = gr.GetGaussian ();
				minionsEtc [this.current].Reset ();
			}
			return result;
		}

		public override bool FizzledOut {
			get {
				return this.timeKilled >= Duration;
			}
		}
	}

	class Gaussianator
	{
		private double nextNextGaussian;
		private bool haveNextNextGaussian;
		private readonly Random rand;
		private readonly int range;

		public Gaussianator (int range)
		{
			this.nextNextGaussian = 0;
			this.haveNextNextGaussian = false;
			this.rand = new Random ();
			this.range = range;
		}

		public int GetGaussian ()
		{  // ported from Oracle Java
			if (haveNextNextGaussian) {
				haveNextNextGaussian = false;
				if (nextNextGaussian > range)
					return range;
				return (int)(nextNextGaussian * range / 4); // standard deviations
			} else {
				double v1, v2, s;
				do {
					v1 = rand.NextDouble ();
					v2 = rand.NextDouble ();
					s = v1 * v1 + v2 * v2;
				} while (s >= 1 || s == 0);
				double multiplier = Math.Sqrt (-2 * Math.Log (s) / s);
				nextNextGaussian = this.range * v2 * multiplier;
				haveNextNextGaussian = true;
				double res = v1 * multiplier;
				if (res > range)
					return range;
				return (int)(res * this.range / 4); // standard deviations
			}

		}
	}

}

