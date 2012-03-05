using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PlayerDiggingPacket : Packet
	{
		public sbyte Status;
		public Int32 X;
		public sbyte Y;
		public Int32 Z;
		public sbyte Face;

		public PlayerDiggingPacket(sbyte status = 0, Int32 x = 0, sbyte y = 0, Int32 z = 0, sbyte face = 0) : base(PacketType.PlayerDigging)
		{
			Status = status;
			X = x;
			Y = y;
			Z = z;
			Face = face;
		}
	}
}
