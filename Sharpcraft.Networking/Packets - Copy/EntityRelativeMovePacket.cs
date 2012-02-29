using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityRelativeMovePacket : Packet
	{
		public Int32 EntityID;
		public sbyte X;
		public sbyte Y;
		public sbyte Z;

		public EntityRelativeMovePacket(Int32 entityId = 0, sbyte x = 0, sbyte y = 0, sbyte z = 0) : base(PacketType.EntityRelativeMove)
		{
			EntityID = entityId;
			X = x;
			Y = y;
			Z = z;
		}
	}
}
