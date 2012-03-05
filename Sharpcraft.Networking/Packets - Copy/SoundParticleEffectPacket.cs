using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class SoundParticleEffectPacket : Packet
	{
		public Int32 EffectID;
		public Int32 X;
		public sbyte Y;
		public Int32 Z;
		public Int32 Data;

		public SoundParticleEffectPacket(Int32 effectId = 0, Int32 x = 0, sbyte y = 0, Int32 z = 0, Int32 data = 0)
			: base(PacketType.SoundParticleEffect)
		{
			EffectID = effectId;
			X = x;
			Y = y;
			Z = z;
			Data = data;
		}
	}
}
