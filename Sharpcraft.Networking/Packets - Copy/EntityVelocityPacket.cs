using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityVelocityPacket : Packet
	{
		public Int32 EntityID;
		public Int16 VelocityX;
		public Int16 VelocityY;
		public Int16 VelocityZ;

		public EntityVelocityPacket(Int32 entityId = 0, Int16 velocityX = 0, Int16 velocityY = 0, Int16 velocityZ = 0) : base(PacketType.EntityVelocity)
		{
			EntityID = entityId;
			VelocityX = velocityX;
			VelocityY = velocityY;
			VelocityZ = velocityZ;
		}
	}
}
