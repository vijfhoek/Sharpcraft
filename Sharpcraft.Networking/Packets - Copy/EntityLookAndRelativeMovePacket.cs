using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityLookAndRelativeMovePacket : Packet
	{
		public Int32 EntityID;
		public sbyte X;
		public sbyte Y;
		public sbyte Z;
		public sbyte Yaw;
		public sbyte Pitch;

		public EntityLookAndRelativeMovePacket(Int32 entityId = 0, sbyte x = 0, sbyte y = 0, sbyte z = 0, sbyte yaw = 0, sbyte pitch = 0)
			: base(PacketType.EntityLookAndRelativeMove)
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
