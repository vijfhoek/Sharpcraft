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
using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking
{
	// TODO: @Vijfhoek, this class looks very messy right now!
	/// <summary>
	/// The Minecraft protocol.
	/// </summary>
	/// <remarks>http://wiki.vg/Protocol</remarks>
	public class Protocol
	{
		/// <summary>
		/// Log object for this class.
		/// </summary>
		private readonly log4net.ILog _log;

		private readonly TcpClient _client;
		private readonly NetworkStream _stream;
		private readonly NetworkTools _tools;

		// TODO: Use BeginRead() of NetworkStream to run a listener in the background?
		// TODO: Firing an event every time it receives a packet? _DOABLE_?

		/// <summary>
		/// Initialize a new instance of <see cref="Protocol" />.
		/// </summary>
		/// <param name="server">Server address to connect to.</param>
		/// <param name="port">Server port.</param>
		public Protocol(string server, int port)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("Connecting to server.");
			_client = new TcpClient();
			_client.Connect(server, port);
			_log.Debug("Getting stream.");
			_stream = _client.GetStream();
			_log.Debug("Initializing tools.");
			_tools = new NetworkTools(_stream);
		}

		/// <summary>
		/// Convert a string to a byte array.
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <returns>String as a byte array.</returns>
		public byte[] StringToBytes(string str)
		{
			byte[] strLength = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(str.Length));
			List<Byte> bytes = strLength.ToList();

			byte[] bteString = Encoding.BigEndianUnicode.GetBytes(str);
			bytes.AddRange(bteString);

			return bytes.ToArray();
		}

		/// <summary>
		/// Convert a byte array to string.
		/// </summary>
		/// <param name="bytes">The byte array to convert.</param>
		/// <returns>Byte array as a string.</returns>
		public string BytesToString(byte[] bytes)
		{
			byte[] bteStrLength = { bytes[0], bytes[1] };
			int strLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bteStrLength, 0));

			var str = string.Empty;

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
			var type = (PacketType) packetID;
			Packet pack = null;

			switch (type)
			{
				case PacketType.KeepAlive:
					pack = new KeepAlivePacket(_tools.ReadInt32());
					break;
				case PacketType.LoginRequest:
				{
					var packet = new LoginRequestPacketSC(_tools.ReadInt32());

					_tools.StreamSkip(2);
					packet.MapSeed = _tools.ReadInt64();
					packet.Gamemode = _tools.ReadInt32();
					packet.Dimension = (sbyte) _stream.ReadByte();
					packet.Difficulty = (sbyte) _stream.ReadByte();
					packet.WorldHeight = (byte) _stream.ReadByte();
					packet.MaxPlayers = (byte) _stream.ReadByte();

					pack = packet;
				}
				break;
				case PacketType.Handshake:
					pack = new HandshakePacketSC(_tools.ReadString());
					break;
				case PacketType.ChatMessage:
					pack = new ChatMessagePacket(_tools.ReadString());
					break;
				case PacketType.TimeUpdate:
					pack = new TimeUpdatePacket(_tools.ReadInt32());
					break;
				case PacketType.EntityEquipment:
					pack = new EntityEquipmentPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadInt16(), _tools.ReadInt16());
					break;
				case PacketType.SpawnPosition:
					pack = new SpawnPositionPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32());
					break;
			}

			return pack;
		}

		public void SendPacket(Packet packet)
		{
			_log.Debug("Sending packet (ID: " + packet.Type + ")");
		
			PacketType type = packet.Type;
			byte packetID = (byte) packet.Type;

			switch (type)
			{
				case PacketType.KeepAlive:
				{
					_log.Debug("Writing KeepAlive packet...");
					var pack = (KeepAlivePacket) packet;
					_tools.WriteByte(packetID);
					_tools.WriteInt32(pack.KeepAliveID);
				}
				break;
				case PacketType.LoginRequest:
				{
					_log.Debug("Writing Login Request packet...");
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
				break;
				case PacketType.Handshake:
				{
					_log.Debug("Writing Handshake packet.");
					var pack = (HandshakePacketCS)packet;
					_tools.WriteByte(packetID);
					_tools.WriteString(pack.Username);
				}
				break;
				case PacketType.ChatMessage:
				{
					var pack = (ChatMessagePacket)packet;
					_tools.WriteByte(packetID);
					_tools.WriteString(pack.Message);
				}
				break;
				case PacketType.UseEntity:
				{
					var pack = (UseEntityPacket)packet;
					_tools.WriteByte(packetID);
					_tools.WriteInt32(pack.AttackerID);
					_tools.WriteInt32(pack.TargetID);
					_tools.WriteBoolean(pack.IsLeftClick);
				}
				break;
			}

			_log.Debug("Sending packet...");
			_stream.Flush();
			_log.Debug("Packet sent!");
		}
	}
}
