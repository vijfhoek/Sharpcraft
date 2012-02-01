using System;
using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class RespawnPacket : Packet
	{
		public sbyte Dimension;
		public sbyte Difficulty;
		public sbyte Creative;
		public Int16 WorldHeight;
		public Int64 MapSeed;
		public string LevelType;

		public RespawnPacket(sbyte dimension = 0, sbyte difficulty = 0, sbyte creative = 0, short height = 0, long seed = 0, string levelType = null)
			: base(PacketType.Respawn)
		{
			Dimension = dimension;
			Difficulty = difficulty;
			Creative = creative;
			WorldHeight = height;
			MapSeed = seed;
			LevelType = levelType;
		}
	}
}
