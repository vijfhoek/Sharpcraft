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

namespace Sharpcraft.Protocol
{
	public enum EndianType
	{
		LittleEndian,
		BigEndian
	}

	public class Protocol
	{
		private readonly TcpClient _client = new TcpClient();
		private readonly NetworkStream _stream;
		private readonly NetworkTools _tools;

		public Protocol(string server, int port)
		{
			_client.Connect(server, port);
			_stream = _client.GetStream();
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
				var packet = new PacketKeepAlive { PacketID = 0x00 };
				packet.KeepAliveID = _tools.ReadInt32();
				pack = packet;
			}
			else if (packetID == 0x01) // Login Request
			{
				var packet = new PacketLoginRequestSC { PacketID = 0x01 };

				packet.EntityID = _tools.ReadInt32();
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
				var packet = new PacketHandshakeSC { PacketID = 0x02 };
				packet.ConnectionHash = _tools.ReadString();
				pack = packet;
			}
			else if (packetID == 0x03) // Chat Message
			{
				var packet = new PacketChatMessage { PacketID = 0x03 };
				packet.Message = _tools.ReadString();
				pack = packet;
			}
			else if (packetID == 0x04) // Time Update
			{
				var packet = new PacketTimeUpdate { PacketID = 0x04 };
				packet.Time = _tools.ReadInt32();
				pack = packet;
			}
			else if (packetID == 0x05) // Entity Equipment
			{
				var packet = new PacketEntityEquipment { PacketID = 0x05 };

				packet.EntityID = _tools.ReadInt32();
				packet.Slot = _tools.ReadInt16();
				packet.ItemID = _tools.ReadInt16();
				packet.Damage = _tools.ReadInt16();

				pack = packet;
			}
			else if (packetID == 0x06) // Spawn Position
			{
				var packet = new PacketSpawnPosition() { PacketID = 0x06 };

				packet.X = _tools.ReadInt32();
				packet.Y = _tools.ReadInt32();
				packet.Z = _tools.ReadInt32();

				pack = packet;
			}

			return pack;
		}

		public void SendPacket(Packet packet)
		{
			byte packetID = packet.PacketID;

			if (packetID == 0x00) // Keep Alive
			{
				var pack = (PacketKeepAlive)packet;
				_tools.WriteByte(packetID);
				_tools.WriteInt32(pack.KeepAliveID);
			}
			else if (packetID == 0x01) // Login Request (Client -> Server)
			{
				var pack = (PacketLoginRequestCS)packet;
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
				var pack = (PacketHandshakeCS)packet;
				_tools.WriteByte(packetID);
				_tools.WriteString(pack.Username);
			}
			else if (packetID == 0x03) // Chat Message
			{
				var pack = (PacketChatMessage)packet;
				_tools.WriteByte(packetID);
				_tools.WriteString(pack.Message);
			}
			else if (packetID == 0x07) // Use Entity
			{
				var pack = (PacketUseEntity)packet;
				_tools.WriteByte(packetID);
				_tools.WriteInt32(pack.AttackerID);
				_tools.WriteInt32(pack.TargetID);
				_tools.WriteBoolean(pack.IsLeftClick);
			}

			_stream.Flush();
		}
	}
}
