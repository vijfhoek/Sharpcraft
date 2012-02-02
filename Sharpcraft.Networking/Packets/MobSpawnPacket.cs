using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class MobSpawnPacket : Packet
	{
		public Int32 EntityID;
		public sbyte MobType;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public sbyte Yaw;
		public sbyte Pitch;
		public object Metadata; // TODO: Implement metadata class! http://wiki.vg/Entities

		public MobSpawnPacket(Int32 entityId = 0, sbyte mobType = 0, Int32 x = 0, Int32 y = 0, Int32 z = 0,
			sbyte yaw = 0, sbyte pitch = 0, object metadata = null) : base(PacketType.MobSpawn)
		{
			EntityID = entityId;
			MobType = mobType;
			X = x;
			Y = y;
			Z = z;
			Yaw = yaw;
			Pitch = pitch;
			Metadata = metadata;
		}
	}
}
