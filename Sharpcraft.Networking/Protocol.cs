/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Net.Sockets;

using Sharpcraft.Logging;
using Sharpcraft.Networking.Enums;
using Sharpcraft.Networking.Packets;

namespace Sharpcraft.Networking
{
	// NOTE: This class is a bit cleaner now!
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
		/// Gets a packet from the server and returns it.
		/// </summary>
		/// <returns>The received packet.</returns>
		public Packet GetPacket()
		{
			var packetID = (byte)_stream.ReadByte();
			_log.Debug("Got packet ID: " + packetID); // Spammy debug message
			var type = (PacketType) packetID;
			Packet pack = null;

			switch (type)
			{
				case PacketType.KeepAlive:
					pack = new KeepAlivePacket(_tools.ReadInt32());
					break;
				case PacketType.LoginRequest:
					var packet = new LoginRequestPacketSC();

					packet.EntityID = _tools.ReadInt32();
					packet.NotUsed = _tools.ReadString();
					packet.MapSeed = _tools.ReadInt64();
					packet.LevelType = _tools.ReadString();
					packet.Gamemode = _tools.ReadInt32();
					packet.Dimension = (sbyte) _stream.ReadByte();
					packet.Difficulty = (sbyte) _stream.ReadByte();
					packet.WorldHeight = (byte) _stream.ReadByte();
					packet.MaxPlayers = (byte) _stream.ReadByte();

					pack = packet;
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
				case PacketType.UseEntity:
					pack = new UseEntityPacket(_tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadBoolean());
					break;
				case PacketType.UpdateHealth:
					pack = new UpdateHealthPacket(_tools.ReadInt16(), _tools.ReadInt16(), _tools.ReadSingle());
					break;
				case PacketType.Respawn:
					pack = new RespawnPacket(_tools.ReadSignedByte(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                         _tools.ReadInt16(), _tools.ReadInt64(), _tools.ReadString());
					break;
				case PacketType.Player:
					pack = new PlayerPacket(_tools.ReadBoolean());
					break;
				case PacketType.PlayerPosition:
					pack = new PlayerPositionPacket(_tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadDouble(),
					                                _tools.ReadBoolean());
					break;
				case PacketType.PlayerLook:
					pack = new PlayerLookPacket(_tools.ReadSingle(), _tools.ReadSingle(), _tools.ReadBoolean());
					break;
				case PacketType.DisconnectKick:
					pack = new DisconnectKickPacket(_tools.ReadString());
					break;
			}

			return pack;
		}

		/// <summary>
		/// Sends the given packet to the connected Minecraft server.
		/// </summary>
		/// <param name="packet">The packet to send</param>
		public void SendPacket(Packet packet)
		{
			_log.Debug("Sending packet (ID: " + packet.Type + ")");
		
			var type = packet.Type;
			var packetID = (byte) packet.Type;

			switch (type)
			{
				case PacketType.KeepAlive:
					{
						_log.Debug("Writing KeepAlive packet (" + ((KeepAlivePacket)packet).KeepAliveID + ")...");
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
						_tools.WriteString(String.Empty);           // Not Used
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
