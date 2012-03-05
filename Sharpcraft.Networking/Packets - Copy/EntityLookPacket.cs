using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityLookPacket : Packet
	{
		public Int32 EntityID;
		public sbyte Yaw;
		public sbyte Pitch;

		public EntityLookPacket(Int32 entityId = 0, sbyte yaw = 0, sbyte pitch = 0) : base(PacketType.EntityLook)
		{
			EntityID = entityId;
			Yaw = yaw;
			Pitch = pitch;
		}
	}
}
