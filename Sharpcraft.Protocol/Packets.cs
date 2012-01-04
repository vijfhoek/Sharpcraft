using System;

namespace Sharpcraft.Protocol
{
	public class Packet
	{
		public byte PacketID;
	}

	public class Packet0 : Packet
	{
		public Int32 KeepAliveID;
	}

	public class Packet1 : Packet
	{
		public Int32 EntityID;
		public Int64 MapSeed;
		public Int32 Gamemode;
		public sbyte Dimension;
		public sbyte Difficulty;
		public byte WorldHeight;
		public byte MaxPlayers;
	}

	public class Packet2 : Packet
	{
		public string ConnectionHash;
	}
}