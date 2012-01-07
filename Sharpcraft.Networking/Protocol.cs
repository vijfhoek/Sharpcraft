/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Sharpcraft.Logging;

namespace Sharpcraft.Networking
{
	// NOTE: This is never used, Vijfhoek?
	public enum EndianType
	{
		LittleEndian,
		BigEndian
	}

	public class Protocol
	{
		private readonly log4net.ILog _log;

		private readonly TcpClient _client = new TcpClient();
		private readonly NetworkStream _stream;
		private readonly NetworkTools _tools;

		public Protocol(string server, int port)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("Connecting to server.");
			_client.Connect(server, port);
			_log.Debug("Getting stream.");
			_stream = _client.GetStream();
			_log.Debug("Initializing tools.");
			_tools = new NetworkTools(_stream);
		}


		public byte[] StringToBytes(string str)
		{
			byte[] strLength = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(str.Length));
			List<Byte> bytes = strLength.ToList();

			byte[] bteString = Encoding.BigEndianUnicode.GetBytes(str);
			bytes.AddRange(bteString);

			return bytes.ToArray();
		}

		public string BytesToString(byte[] bytes)
		{
			byte[] bteStrLength = { bytes[0], bytes[1] };
			int strLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bteStrLength, 0));

			string str = "";

			for (short s = 1; s < strLength + 1; s++)
			{
				byte[] tmp = { bytes[s * 2], bytes[(s * 2) + 1] };
				str += Encoding.BigEndianUnicode.GetString(tmp);
			}

			return str;
		}


		public Packet GetPacket()
		{
			var packetID = (byte)_stream.ReadByte();
			Packet pack = null;

			if (packetID == 0x00) // Keep Alive
			{
				var packet = new KeepAlivePacket { PacketID = 0x00, KeepAliveID = _tools.ReadInt32() };
				pack = packet;
			}
			else if (packetID == 0x01) // Login Request
			{
				var packet = new LoginRequestPacketSC { PacketID = 0x01, EntityID = _tools.ReadInt32() };

				_tools.StreamSkip(2);
				packet.MapSeed = _tools.ReadInt64();
				packet.Gamemode = _tools.ReadInt32();
				packet.Dimension = (sbyte)_stream.ReadByte();
				packet.Difficulty = (sbyte)_stream.ReadByte();
				packet.WorldHeight = (byte)_stream.ReadByte();
				packet.MaxPlayers = (byte)_stream.ReadByte();

				pack = packet;
			}
			else if (packetID == 0x02) // Handshake
			{
				var packet = new HandshakePacketSC { PacketID = 0x02, ConnectionHash = _tools.ReadString() };
				pack = packet;
			}
			else if (packetID == 0x03) // Chat Message
			{
				var packet = new ChatMessagePacket { PacketID = 0x03, Message = _tools.ReadString() };
				pack = packet;
			}
			else if (packetID == 0x04) // Time Update
			{
				var packet = new TimeUpdatePacket { PacketID = 0x04, Time = _tools.ReadInt32() };
				pack = packet;
			}
			else if (packetID == 0x05) // Entity Equipment
			{
				var packet = new EntityEquipmentPacket
				{
					PacketID = 0x05,
					EntityID = _tools.ReadInt32(),
					Slot = _tools.ReadInt16(),
					ItemID = _tools.ReadInt16(),
					Damage = _tools.ReadInt16()
				};

				pack = packet;
			}
			else if (packetID == 0x06) // Spawn Position
			{
				var packet = new SpawnPositionPacket
				{
					PacketID = 0x06,
					X = _tools.ReadInt32(),
					Y = _tools.ReadInt32(),
					Z = _tools.ReadInt32()
				};

				pack = packet;
			}

			return pack;
		}

		public void SendPacket(Packet packet)
		{
			_log.Debug("Sending packet (ID: " + packet.PacketID + ")");

			byte packetID = packet.PacketID;

			if (packetID == 0x00) // Keep Alive
			{
				_log.Debug("Sending KeepAlive packet.");
				var pack = (KeepAlivePacket)packet;
				_tools.WriteByte(packetID);
				_tools.WriteInt32(pack.KeepAliveID);
			}
			else if (packetID == 0x01) // Login Request (Client -> Server)
			{
				_log.Debug("Sending Login Request packet.");
				var pack = (LoginRequestPacketCS)packet;
				_tools.WriteByte(packetID);
				_tools.WriteInt32(pack.ProtocolVersion);
				_tools.WriteString(pack.Username);
				_tools.WriteInt64(0);						// Not Used
				_tools.WriteInt32(0);						// Not Used
				_tools.WriteByte(0);						// Not Used
				_tools.WriteByte(0);						// Not Used
				_tools.WriteByte(0);						// Not Used
				_tools.WriteByte(0);						// Not Used
			}
			else if (packetID == 0x02) // Handshake (Client -> Server)
			{
				_log.Debug("Sending Handshake packet.");
				var pack = (HandshakePacketCS)packet;
				_tools.WriteByte(packetID);
				_tools.WriteString(pack.Username);
			}
			else if (packetID == 0x03) // Chat Message
			{
				var pack = (ChatMessagePacket)packet;
				_tools.WriteByte(packetID);
				_tools.WriteString(pack.Message);
			}
			else if (packetID == 0x07) // Use Entity
			{
				var pack = (UseEntityPacket)packet;
				_tools.WriteByte(packetID);
				_tools.WriteInt32(pack.AttackerID);
				_tools.WriteInt32(pack.TargetID);
				_tools.WriteBoolean(pack.IsLeftClick);
			}

			_stream.Flush();
			_log.Debug("Packet sent!");
		}
	}
}