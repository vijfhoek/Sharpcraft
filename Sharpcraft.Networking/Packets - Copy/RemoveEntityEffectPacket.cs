using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class RemoveEntityEffectPacket : Packet
	{
		public Int32 EntityID;
		public sbyte EffectID;

		public RemoveEntityEffectPacket(Int32 entityId = 0, sbyte effectId = 0) : base(PacketType.RemoveEntityEffect)
		{
			EntityID = entityId;
			EffectID = effectId;
		}
	}
}
