using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class NamedEntitySpawnPacket : Packet
	{
		public Int32 EntityID;
		public string PlayerName;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public sbyte Rotation;
		public sbyte Pitch;
		public Int16 CurrentItem;

		public NamedEntitySpawnPacket(Int32 entityId = 0, string playerName = null, Int32 x = 0, Int32 y = 0, Int32 z = 0,
			sbyte rotation = 0, sbyte pitch = 0, Int16 currentItem = 0) : base(PacketType.NamedEntitySpawn)
		{
			EntityID = entityId;
			PlayerName = playerName;
			X = x;
			Y = y;
			Z = z;
			Rotation = rotation;
			Pitch = pitch;
			CurrentItem = currentItem;
		}
	}
}
