using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class BlockChangePacket : Packet
	{
		public Int32 X;
		public sbyte Y;
		public Int32 Z;
		public sbyte BlockType;
		public sbyte Metadata; // Should this be of type Metadata instead?

		public BlockChangePacket(Int32 x = 0, sbyte y = 0, Int32 z = 0, sbyte blockType = 0, sbyte metadata = 0)
			: base(PacketType.BlockChange)
		{
			X = x;
			Y = y;
			Z = z;
			BlockType = blockType;
			Metadata = metadata;
		}
	}
}
