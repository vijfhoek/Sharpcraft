using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class AddObjectVehiclePacket : Packet
	{
		public Int32 EntityID;
		public sbyte ObjectType;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public Int32 FireballThrowerID;
		public Int16 SpeedX;
		public Int16 SpeedY;
		public Int16 SpeedZ;

		public AddObjectVehiclePacket(Int32 entityId = 0, sbyte objectType = 0, Int32 x = 0, Int32 y = 0, Int32 z = 0,
			Int32 fireballThrowerId = 0, Int16 speedX = 0, Int16 speedY = 0, Int16 speedZ = 0) : base(PacketType.AddObjectVehicle)
		{
			EntityID = entityId;
			ObjectType = objectType;
			X = x;
			Y = y;
			Z = z;
			FireballThrowerID = fireballThrowerId;
			SpeedX = speedX;
			SpeedY = speedY;
			SpeedZ = speedZ;
		}
	}
}
