using System;
using System.Collections.Generic;
using Musicinator.Model;
using Musicinator.Model.Impl;


namespace Musicinator.Processor.Impl
{
	public class MidiMinionGrinder: IGrinder
	{
		private List< IMinion> minions;

		public MidiMinionGrinder (int channel)
		{
			this.minions = new List<IMinion> ();
			this.Channel = channel;
		}

		public int Channel { get; set; }

		public void Grind (IMinion minion)
		{
			this.minions.Add (minion);
		}

		public byte[] ExtractJuices ()
		{
			byte[] result = new byte[this.minions.Count * 3];
			int ix = 0;
			this.minions.ForEach (minion => {
				ProcessMinion (minion).CopyTo (result, ix);
				ix += 3;
			});
			this.minions = new List<IMinion> ();
			return result;
		}

		private byte[] ProcessMinion (IMinion minion)
		{
			if (minion is NoteStart) {
				NoteStart m = minion as NoteStart;
				return new byte[]{ (byte)(Control.NoteOn + Channel), (byte)m.NotePitch, (byte)m.Velocity };
			} else if (minion is NoteEnd) {
				NoteEnd m = minion as NoteEnd;
				return new byte[]{ (byte)(Control.NoteOff + Channel), (byte)m.NotePitch, 0 };
			} else if (minion is DrumStart) {
				DrumStart m = minion as DrumStart;
				return new byte[]{ (byte)(Control.NoteOn + Channel), (byte)(35 + m.DrumPitch), (byte)m.Velocity };
			} else if (minion is DrumEnd) {
				DrumEnd m = minion as DrumEnd;
				return new byte[]{ (byte)(Control.NoteOff + Channel), (byte)(35 + m.DrumPitch), 0 };
			} else if (minion is InstrumentChange) {
				InstrumentChange m = minion as InstrumentChange;
				return new byte[]{ (byte)(Control.Program + Channel), (byte)m.NewInstrument, 0 };
			} 
			throw new InvalidOperationException ("Minion not recognised for processing: " + minion);
		}
	}

	public enum Control
	{
		BankSelect = 0x00,
		Modulation = 0x01,
		Breath = 0x02,
		Foot = 0x04,
		PortamentoTime = 0x05,
		DteMsb = 0x06,
		Volume = 0x07,
		Balance = 0x08,
		Pan = 0x0A,
		Expression = 0x0B,
		EffectControl1 = 0x0C,
		EffectControl2 = 0x0D,
		General1 = 0x10,
		General2 = 0x11,
		General3 = 0x12,
		General4 = 0x13,
		BankSelectLsb = 0x20,
		ModulationLsb = 0x21,
		BreathLsb = 0x22,
		FootLsb = 0x24,
		PortamentoTimeLsb = 0x25,
		DteLsb = 0x26,
		VolumeLsb = 0x27,
		BalanceLsb = 0x28,
		PanLsb = 0x2A,
		ExpressionLsb = 0x2B,
		Effect1Lsb = 0x2C,
		Effect2Lsb = 0x2D,
		General1Lsb = 0x30,
		General2Lsb = 0x31,
		General3Lsb = 0x32,
		General4Lsb = 0x33,
		Hold = 0x40,
		PortamentoSwitch = 0x41,
		Sostenuto = 0x42,
		SoftPedal = 0x43,
		Legato = 0x44,
		Hold2 = 0x45,
		SoundController1 = 0x46,
		SoundController2 = 0x47,
		SoundController3 = 0x48,
		SoundController4 = 0x49,
		SoundController5 = 0x4A,
		SoundController6 = 0x4B,
		SoundController7 = 0x4C,
		SoundController8 = 0x4D,
		SoundController9 = 0x4E,
		SoundController10 = 0x4F,
		General5 = 0x50,
		General6 = 0x51,
		General7 = 0x52,
		General8 = 0x53,
		PortamentoControl = 0x54,
		Rsd = 0x5B,
		Effect1 = 0x5B,
		Tremolo = 0x5C,
		Effect2 = 0x5C,
		Csd = 0x5D,
		Effect3 = 0x5D,
		Celeste = 0x5E,
		Effect4 = 0x5E,
		Phaser = 0x5F,
		Effect5 = 0x5F,
		DteIncrement = 0x60,
		DteDecrement = 0x61,
		NrpnLsb = 0x62,
		NrpnMsb = 0x63,
		RpnLsb = 0x64,
		RpnMsb = 0x65,
		// Channel mode messages
		AllSoundOff = 0x78,
		ResetAllControllers = 0x79,
		LocalControl = 0x7A,
		AllNotesOff = 0x7B,
		OmniModeOff = 0x7C,
		OmniModeOn = 0x7D,
		PolyModeOnOff = 0x7E,
		PolyModeOn = 0x7F,
		NoteOff = 0x80,
		NoteOn = 0x90,
		PAf = 0xA0,
		CC = 0xB0,
		Program = 0xC0,
		CAf = 0xD0,
		Pitch = 0xE0,
		SysEx1 = 0xF0,
		SysEx2 = 0xF7,
		Meta = 0xFF
	}


}

