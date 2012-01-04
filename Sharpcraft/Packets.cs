using System;

namespace Sharpcraft
{
	public class Packet
	{
		public byte packetID;
	}

	public class Packet0 : Packet
	{
		public Int32 keepAliveID;
	}

	public class Packet1 : Packet
	{
		public Int32 entityID;
		public Int64 mapSeed;
		public Int32 gamemode;
		public sbyte dimension;
		public sbyte difficulty;
		public byte worldHeight;
		public byte maxPlayers;
	}

	public class Packet2 : Packet
	{
		public string connectionHash;
	}
}