using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class BlockActionPacket : Packet
	{
		public Int32 X;
		public Int16 Y;
		public Int32 Z;
		public sbyte Byte1;
		public sbyte Byte2;

		public BlockActionPacket(Int32 x = 0, Int16 y = 0, Int32 z = 0, sbyte byte1 = 0, sbyte byte2 = 0)
			: base(PacketType.BlockAction)
		{
			X = x;
			Y = y;
			Z = z;
			Byte1 = byte1;
			Byte2 = byte2;
		}
	}
}
