/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Net.Sockets;
using LibNbt;
using Sharpcraft.Library.Minecraft;
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

		private readonly TcpClient _tcpClient;
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
			_tcpClient = new TcpClient();
			_tcpClient.Connect(server, port);
			_log.Debug("Getting stream.");
			_stream = _tcpClient.GetStream();
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
			//_log.Debug("Got packet ID: " + packetID); // Spammy debug message
			if (!Enum.IsDefined(typeof(PacketType), packetID))
				return null;
			var type = (PacketType) packetID;
			Packet pack = null;

			switch (type)
			{
				case PacketType.KeepAlive:
					pack = new KeepAlivePacket(_tools.ReadInt32());
					break;
				case PacketType.LoginRequest:
					pack = new LoginRequestPacketSC
					{
						EntityID = _tools.ReadInt32(),
						NotUsed = _tools.ReadString(),
						MapSeed = _tools.ReadInt64(),
						LevelType = _tools.ReadString(),
						Gamemode = _tools.ReadInt32(),
						Dimension = (sbyte) _stream.ReadByte(),
						Difficulty = (sbyte) _stream.ReadByte(),
						WorldHeight = (byte) _stream.ReadByte(),
						MaxPlayers = (byte) _stream.ReadByte()
					};
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
				case PacketType.PlayerPositionAndLook:
					pack = new PlayerPositionAndLookPacket(_tools.ReadDouble(), _tools.ReadDouble(), _tools.ReadDouble(),
					                                       _tools.ReadDouble(), _tools.ReadSingle(), _tools.ReadSingle(),
					                                       _tools.ReadBoolean());
					break;
				case PacketType.PlayerDigging:
					pack = new PlayerDiggingPacket(_tools.ReadSignedByte(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                               _tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.PlayerBlockPlacement:
					pack = new PlayerBlockPlacementPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(),
					                                      _tools.ReadSignedByte(), _tools.ReadSlotData());
					break;
				case PacketType.UseBed:
					pack = new UseBedPacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                        _tools.ReadInt32());
					break;
				case PacketType.Animation:
					pack = new AnimationPacket(_tools.ReadInt32(), _tools.ReadSignedByte());
					break;
				case PacketType.NamedEntitySpawn:
					pack = new NamedEntitySpawnPacket(_tools.ReadInt32(), _tools.ReadString(), _tools.ReadInt32(), _tools.ReadInt32(),
					                                  _tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadSignedByte(),
					                                  _tools.ReadInt16());
					break;
				case PacketType.PickupSpawn:
					pack = new PickupSpawnPacket(_tools.ReadInt32(), _tools.ReadInt16(), _tools.ReadSignedByte(), _tools.ReadInt16(),
					                             _tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadSignedByte(),
					                             _tools.ReadSignedByte(), _tools.ReadSignedByte());
					break;
				case PacketType.CollectItem:
					pack = new CollectItemPacket(_tools.ReadInt32(), _tools.ReadInt32());
					break;
				case PacketType.AddObjectVehicle:
					pack = new AddObjectVehiclePacket(_tools.ReadInt32(), _tools.ReadSignedByte(), _tools.ReadInt32(), _tools.ReadInt32(), _tools.ReadInt32());
					var ftEid = _tools.ReadInt32(); ((AddObjectVehiclePacket)pack).FireballThrowerID = ftEid;
					if (ftEid > 0)
					{
						var aovpPack = (AddObjectVehiclePacket) pack;
						aovpPack.SpeedX = _tools.ReadInt16();
						aovpPack.SpeedY = _tools.ReadInt16();
						aovpPack.SpeedZ = _tools.ReadInt16();
					}
					break;
				case PacketType.HoldingChange:
					pack = null;
					break;
				case PacketType.MobSpawn:
					pack = null;
					break;
				case PacketType.EntityPainting:
					pack = null;
					break;
				case PacketType.ExperienceOrb:
					pack = null;
					break;
				case PacketType.EntityVelocity:
					pack = null;
					break;
				case PacketType.DestroyEntity:
					pack = null;
					break;
				case PacketType.Entity:
					pack = null;
					break;
				case PacketType.EntityRelativeMove:
					pack = null;
					break;
				case PacketType.EntityLook:
					pack = null;
					break;
				case PacketType.EntityLookAndRelativeMove:
					pack = null;
					break;
				case PacketType.EntityTeleport:
					pack = null;
					break;
				case PacketType.EntityStatus:
					pack = null;
					break;
				case PacketType.AttachEntity:
					pack = null;
					break;
				case PacketType.EntityMetadata:
					pack = null;
					break;
				case PacketType.EntityEffect:
					pack = null;
					break;
				case PacketType.RemoveEntityEffect:
					pack = null;
					break;
				case PacketType.Experience:
					pack = null;
					break;
				case PacketType.PreChunk:
					pack = null;
					break;
				case PacketType.MapChunk:
					pack = null;
					break;
				case PacketType.MultiBlockChange:
					pack = null;
					break;
				case PacketType.BlockChange:
					pack = null;
					break;
				case PacketType.BlockAction:
					pack = null;
					break;
				case PacketType.Explosion:
					pack = null;
					break;
				case PacketType.SoundParticleEffect:
					pack = null;
					break;
				case PacketType.NewInvalidState:
					pack = null;
					break;
				case PacketType.Thunderbolt:
					pack = null;
					break;
				case PacketType.OpenWindow:
					pack = null;
					break;
				case PacketType.CloseWindow:
					pack = null;
					break;
				case PacketType.SetSlot:
					pack = null;
					break;
				case PacketType.WindowItems:
					pack = null;
					break;
				case PacketType.UpdateWindowProperty:
					pack = null;
					break;
				case PacketType.Transaction:
					pack = null;
					break;
				case PacketType.CreativeInventoryAction:
					pack = null;
					break;
				case PacketType.UpdateSign:
					pack = null;
					break;
				case PacketType.ItemData:
					pack = null;
					break;
				case PacketType.IncrementStatistic:
					pack = null;
					break;
				case PacketType.PlayerListItem:
					pack = null;
					break;
				case PacketType.PluginMessage:
					pack = null;
					break;
				case PacketType.ServerListPing:
					pack = null;
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
						var pack = (KeepAlivePacket) packet;
						_log.Debug("Writing KeepAlive packet (" + pack.KeepAliveID + ")...");
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
