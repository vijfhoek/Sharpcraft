using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityStatusPacket : Packet
	{
		public Int32 EntityID;
		public sbyte Status;

		public EntityStatusPacket(Int32 entityId = 0, sbyte status = 0) : base(PacketType.EntityStatus)
		{
			EntityID = entityId;
			Status = status;
		}
	}
}
