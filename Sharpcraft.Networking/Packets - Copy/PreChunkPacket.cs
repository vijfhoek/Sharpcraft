using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PreChunkPacket : Packet
	{
		public Int32 X;
		public Int32 Z;
		public bool Mode;

		public PreChunkPacket(Int32 x = 0, Int32 z = 0, bool mode = false) : base(PacketType.PreChunk)
		{
			X = x;
			Z = z;
			Mode = mode;
		}
	}
}
