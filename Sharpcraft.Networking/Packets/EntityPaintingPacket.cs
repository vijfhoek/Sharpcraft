using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityPaintingPacket : Packet
	{
		public Int32 EntityID;
		public string Title;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public Int32 Direction;

		public EntityPaintingPacket(Int32 entityId = 0, string title = null, Int32 x = 0, Int32 y = 0, Int32 z = 0, Int32 direction = 0) : base(PacketType.EntityPainting)
		{
			EntityID = entityId;
			Title = title;
			X = x;
			Y = y;
			Z = z;
			Direction = direction;
		}
	}
}
