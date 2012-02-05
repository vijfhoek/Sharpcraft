using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class MultiBlockChangePacket : Packet
	{
		public Int32 ChunkX;
		public Int32 ChunkZ;
		public Int16 ArraySize;
		public Int16[,] Coordinates;
		public sbyte[] Types;
		public sbyte[] Metadata; // Should this be a Metadata array?

		public MultiBlockChangePacket(Int32 chunkX = 0, Int32 chunkZ = 0, Int16 arraySize = 0, Int16[,] coordinates = null,
			sbyte[] types = null, sbyte[] metadata = null) : base(PacketType.MultiBlockChange)
		{
			ChunkX = chunkX;
			ChunkZ = chunkZ;
			ArraySize = arraySize;
			Coordinates = coordinates;
			Types = types;
			Metadata = metadata;
		}
	}
}
