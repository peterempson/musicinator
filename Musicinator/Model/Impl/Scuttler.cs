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
		private long duration;
		private int[] index;
		private int ix;

		public Scuttler (int notes, params IGang[] minions) : base (minions)
		{
			this.duration = TimeSignature.GetTicksForNote (notes);
			Gaussianator gr = new Gaussianator (this.minionsEtc.Count - 1);

			this.index = Enumerable
				.Repeat (0, 50)  // TODO: 50? praps calculate this somehow
				.Select (i => gr.GetGaussian ())
				.ToArray ();
			
			this.ix = 0;
			this.current = this.index [ix];
		}

		public override long Duration { get { return this.duration; } }

		protected override void UpdateCurrent ()
		{
			this.ix++;
			this.current = this.index [ix];
		}

		public override void Reset ()
		{
			base.Reset ();
			this.ix = 0;
			this.current = this.index [ix];
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

