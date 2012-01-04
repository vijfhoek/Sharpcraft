﻿using System;

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

	public class Packet3 : Packet
	{
		public string Message;
	}

	public class Packet4 : Packet
	{
		public long Time;
	}

	public class Packet5 : Packet
	{
		public int EntityID;
		public short Slot;
		public short ItemID;
		public short Damage;
	}
}