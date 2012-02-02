using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class MultiBlockChangePacket : Packet
	{
		public Int32 ChunkX;
		public Int32 ChunkZ;
		public Int16 Size;
		public Int16[] Coordinates;
		public byte[] Types;
		public byte[] Metadata; // Should this be a Metadata array?

		public MultiBlockChangePacket(Int32 chunkX = 0, Int32 chunkZ = 0, Int16 size = 0, Int16[] coordinates = null,
			byte[] types = null, byte[] metadata = null) : base(PacketType.MultiBlockChange)
		{
			ChunkX = chunkX;
			ChunkZ = chunkZ;
			Size = size;
			Coordinates = coordinates;
			Types = types;
			Metadata = metadata;
		}
	}
}
