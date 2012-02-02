using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityEffectPacket : Packet
	{
		public Int32 EntityID;
		public sbyte EffectID;
		public sbyte Amplifier;
		public Int16 Duration;

		public EntityEffectPacket(Int32 entityId = 0, sbyte effectId = 0, sbyte amplifier = 0, Int16 duration = 0) : base(PacketType.EntityEffect)
		{
			EntityID = entityId;
			EffectID = effectId;
			Amplifier = amplifier;
			Duration = duration;
		}
	}
}
