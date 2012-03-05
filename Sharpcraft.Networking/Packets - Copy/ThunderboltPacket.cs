using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class ThunderboltPacket : Packet
	{
		public Int32 EntityID;
		public bool Unknown;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;

		public ThunderboltPacket(Int32 entityId = 0, bool unknown = false, Int32 x = 0, Int32 y = 0, Int32 z = 0)
			: base(PacketType.Thunderbolt)
		{
			EntityID = entityId;
			Unknown = unknown;
			X = x;
			Y = y;
			Z = z;
		}
	}
}
