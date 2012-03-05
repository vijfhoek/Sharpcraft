using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityPacket : Packet
	{
		public Int32 EntityID;

		public EntityPacket(Int32 entityId = 0) : base(PacketType.Entity)
		{
			EntityID = entityId;
		}
	}
}
