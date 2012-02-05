using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class MapChunkPacket : Packet
	{
		public Int32 X;
		public Int16 Y;
		public Int32 Z;
		public sbyte SizeX;
		public sbyte SizeY;
		public sbyte SizeZ;
		public Int32 CompressedSize;
		public sbyte[] CompressedData;

		public MapChunkPacket(Int32 x = 0, Int16 y = 0, Int32 z = 0, sbyte sizeX = 0, sbyte sizeY = 0, sbyte sizeZ = 0,
			Int32 compressedSize = 0, sbyte[] compressedData = null) : base(PacketType.MapChunk)
		{
			X = x;
			Y = y;
			Z = z;
			SizeX = sizeX;
			SizeY = sizeY;
			SizeZ = sizeZ;
			CompressedSize = compressedSize;
			CompressedData = compressedData;
		}
	}
}
