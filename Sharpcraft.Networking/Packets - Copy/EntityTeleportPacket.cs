using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityTeleportPacket : Packet
	{
		public Int32 EntityID;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public sbyte Yaw;
		public sbyte Pitch;

		public EntityTeleportPacket(Int32 entityId = 0, Int32 x = 0, Int32 y = 0, Int32 z = 0, sbyte yaw = 0, sbyte pitch = 0)
			: base(PacketType.EntityTeleport)
		{
			EntityID = entityId;
			X = x;
			Y = y;
			Z = z;
			Yaw = yaw;
			Pitch = pitch;
		}
	}
}
