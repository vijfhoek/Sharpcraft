using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class SpawnPositionPacket : Packet
	{
		public Int32 X;
		public Int32 Y;
		public Int32 Z;

		public SpawnPositionPacket(Int32 x = 0, Int32 y = 0, Int32 z = 0) : base(PacketType.SpawnPosition)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
}
