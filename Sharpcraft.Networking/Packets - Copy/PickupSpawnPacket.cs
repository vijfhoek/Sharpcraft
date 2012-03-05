using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PickupSpawnPacket : Packet
	{
		public Int32 EntityID;
		public Int16 Item;
		public sbyte Count;
		public Int16 DamageData;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public sbyte Rotation;
		public sbyte Pitch;
		public sbyte Roll;

		public PickupSpawnPacket(Int32 entityId = 0, Int16 item = 0, sbyte count = 0, Int16 damageData = 0, Int32 x = 0, Int32 y = 0, Int32 z = 0,
			sbyte rotation = 0, sbyte pitch = 0, sbyte roll = 0) : base(PacketType.PickupSpawn)
		{
			EntityID = entityId;
			Item = item;
			Count = count;
			DamageData = damageData;
			X = x;
			Y = y;
			Z = z;
			Rotation = rotation;
			Pitch = pitch;
			Roll = roll;
		}
	}
}
