/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking
{
	public class Packet
	{
		public PacketType Type;

		public Packet(PacketType type)
		{
			Type = type;
		}
	}

	public class KeepAlivePacket : Packet
	{
		public Int32 KeepAliveID;

		public KeepAlivePacket(Int32 id = 0) : base(PacketType.KeepAlive)
		{
			KeepAliveID = id;
		}
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

		public LoginRequestPacketSC(Int32 entityId = 0, Int64 mapSeed = 0, Int32 gamemode = 0,
			sbyte dimension = 0, sbyte difficulty = 0, byte worldHeight = 0, byte maxPlayers = 0) : base(PacketType.LoginRequest)
		{
			EntityID = entityId;
			MapSeed = mapSeed;
			Gamemode = gamemode;
			Dimension = dimension;
			Difficulty = difficulty;
			WorldHeight = worldHeight;
			MaxPlayers = maxPlayers;
		}
	}

	public class LoginRequestPacketCS : Packet
	{
		public Int32 ProtocolVersion;
		public string Username;

		public LoginRequestPacketCS(Int32 protocolVersion = 0, string username = null) : base(PacketType.LoginRequest)
		{
			ProtocolVersion = protocolVersion;
			Username = username;
		}
	}

	public class HandshakePacketSC : Packet
	{
		public string ConnectionHash;

		public HandshakePacketSC(string connectionHash = null) : base(PacketType.Handshake)
		{
			ConnectionHash = connectionHash;
		}
	}

	public class HandshakePacketCS : Packet
	{
		public string Username;

		public HandshakePacketCS(string username = null) : base(PacketType.Handshake)
		{
			Username = username;
		}
	}

	public class ChatMessagePacket : Packet
	{
		public string Message;

		public ChatMessagePacket(string message = null) : base(PacketType.ChatMessage)
		{
			Message = message;
		}
	}

	public class TimeUpdatePacket : Packet
	{
		public Int64 Time;

		public TimeUpdatePacket(Int64 time = 0) : base(PacketType.TimeUpdate)
		{
			Time = time;
		}
	}

	public class EntityEquipmentPacket : Packet
	{
		public Int32 EntityID;
		public Int16 Slot;
		public Int16 ItemID;
		public Int16 Damage;

		public EntityEquipmentPacket(Int32 entityId = 0, Int16 slot = 0, Int16 itemId = 0, Int16 damage = 0) : base(PacketType.EntityEquipment)
		{
			EntityID = entityId;
			Slot = slot;
			ItemID = itemId;
			Damage = damage;
		}
	}

	public class SpawnPositionPacket : Packet
	{
		public Int32 X;
		public Int32 Y;
		public Int32 Z;

		public SpawnPositionPacket(Int32 x = 0, Int32 y = 0, Int32 z = 0) : base(PacketType.SpawnPosition)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}

	public class UseEntityPacket : Packet
	{
		public Int32 AttackerID;
		public Int32 TargetID;
		public bool IsLeftClick;

		public UseEntityPacket(Int32 attackerId = 0, Int32 targetId = 0, bool isLeftClick = false) : base(PacketType.UseEntity)
		{
			AttackerID = attackerId;
			TargetID = targetId;
			IsLeftClick = isLeftClick;
		}
	}
}
