/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;

namespace Sharpcraft.Networking
{
	public class Packet
	{
		public byte PacketID;
	}

	public class KeepAlivePacket : Packet
	{
		public Int32 KeepAliveID;
	}

	public class LoginRequestPacketSC : Packet
	{
		public Int32 EntityID;
		public Int64 MapSeed;
		public Int32 Gamemode;
		public sbyte Dimension;
		public sbyte Difficulty;
		public byte WorldHeight;
		public byte MaxPlayers;
	}

	public class LoginRequestPacketCS : Packet
	{
		public Int32 ProtocolVersion;
		public string Username;
	}

	public class HandshakePacketSC : Packet
	{
		public string ConnectionHash;
	}

	public class HandshakePacketCS : Packet
	{
		public string Username;
	}

	public class ChatMessagePacket : Packet
	{
		public string Message;
	}

	public class TimeUpdatePacket : Packet
	{
		public Int64 Time;
	}

	public class EntityEquipmentPacket : Packet
	{
		public Int32 EntityID;
		public Int16 Slot;
		public Int16 ItemID;
		public Int16 Damage;
	}

	public class SpawnPositionPacket : Packet
	{
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
	}

	public class UseEntityPacket : Packet
	{
		public Int32 AttackerID;
		public Int32 TargetID;
		public bool IsLeftClick;
	}
}
