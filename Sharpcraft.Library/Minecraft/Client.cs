using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

using Sharpcraft.Logging;
using Sharpcraft.Networking;
using Sharpcraft.Networking.Enums;
using Sharpcraft.Networking.Packets;
using Sharpcraft.Library.Minecraft.Entities;

namespace Sharpcraft.Library.Minecraft
{
	public class Client
	{
		private readonly log4net.ILog _log;

		private readonly Protocol _protocol;
		private readonly Server _server;
		private readonly Player _player;
		private World _world;

		private PacketListener _listener;

		/// <summary>
		/// A list of the Items that are available in the game.
		/// </summary>
		public static List<Item> Items { get; private set; }

		public Client(Server server, Player player)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("Minecraft Client created!");
			_server = server;
			_log.Debug("Loading Items from file...");
			try
			{
				using (var reader = new StreamReader(Constants.MinecraftItemFile))
					Items = new JsonSerializer().Deserialize<List<Item>>(new JsonTextReader(reader));
			}
			catch(IOException ex)
			{
				_log.Error("Failed to read item list from file!");
				_log.Error(ex.GetType() + ": " + ex.Message);
				_log.Error("Stack Trace:\n" + ex.StackTrace);
				throw new Exception("Sharpcraft.Library.Minecraft.Client failed to initialize! Could not read item list file!", ex);
			}
			_log.Debug("Creating communication protocol...");
			_protocol = new Protocol(_server.Address, _server.Port);
			_log.Info("Client initiated on " + _server.Address + ":" + _server.Port + "!");
			_player = player;
			_world = new World();
		}

		public static Item GetItemByID(short id)
		{
			return Items.Where(item => item.Id == id).FirstOrDefault();
		}

		public Server GetServer()
		{
			return _server;
		}

		public bool Connect()
		{
			// We need to create a connection thread
			_log.Info("Connecting to " + _server.Address + ":" + _server.Port + "...");
			_protocol.SendPacket(new HandshakePacketCS(_player.Name));
			_log.Info("Waiting for handshake response...");
			Packet response = _protocol.GetPacket();
			if (!(response is HandshakePacketSC))
			{
				_log.Warn("Incorrect packet type sent as response! Expected HandshakePacketSC, got " + response.GetType());
				return false;
			}
			_log.Info("Server responded to Handshake with " + ((HandshakePacketSC)response).ConnectionHash);
			_log.Info("Sending login request...");
			_protocol.SendPacket(new LoginRequestPacketCS(Networking.Constants.ProtocolVersion, _player.Name));
			_log.Info("Waiting for login response...");
			response = _protocol.GetPacket();
			if (!(response is LoginRequestPacketSC))
			{
				_log.Warn("Incorrect packet type sent as response! Expected LoginRequestPacketSC, got " + response.GetType());
				return false;
			}
			_log.Info("Server responded to login with mapseed " + ((LoginRequestPacketSC)response).MapSeed);
			ParseLoginRequestSC((LoginRequestPacketSC) response);
			_log.Info("Creating packet listener...");
			_listener = new PacketListener(_protocol);
			_listener.OnPacketReceived += PacketReceived;
			_log.Info("Further connection methods not yet implemented, halting Connect!");
			return true;
		}

		public bool Disconnect()
		{
			return false; // For now
		}

		public void Exit()
		{
			Disconnect();
			_listener.OnPacketReceived -= PacketReceived;
			_listener.Stop();
		}

		public void SendMessage(string message)
		{
			var packet = new ChatMessagePacket(message);
			_protocol.SendPacket(packet);
		}

		public void SendEmote(string emote)
		{
			
		}

		public void SendCommand(string command)
		{
			
		}

		public Player GetPlayer()
		{
			return _player;
		}

		private void ParseLoginRequestSC(LoginRequestPacketSC packet)
		{
			_log.Debug("Updating world, player and server data...");
			_log.Debug("Setting Player Entity ID to " + packet.EntityID);
			_player.EntityID = packet.EntityID;
			_log.Debug("Setting map seed to " + packet.MapSeed);
			_world.SetSeed(packet.MapSeed);
			_log.Debug("Setting map type to " + packet.LevelType);
			_world.SetLevelType(packet.LevelType);
			_log.Debug("Setting server mode to " + packet.Gamemode);
			_server.SetMode(packet.Gamemode);
			_log.Debug("Setting world dimension to " + packet.Dimension);
			_world.SetDimension(packet.Dimension);
			_log.Debug("Setting world difficulty to " + packet.Difficulty);
			_world.SetDifficulty(packet.Difficulty);
			_log.Debug("Setting world height to " + packet.WorldHeight);
			_world.SetHeight(packet.WorldHeight);
			_log.Debug("Setting server max players to " + packet.MaxPlayers);
			if (_server.Players > packet.MaxPlayers)
				_server.Players = packet.MaxPlayers;
			_server.MaxPlayers = packet.MaxPlayers;
			_log.Debug("World, player and server data successfully updated!");
		}

		private void PacketReceived(object sender, PacketEventArgs e)
		{
			Packet response;
			switch (e.Packet.Type)
			{
				case PacketType.KeepAlive:
					response = new KeepAlivePacket(((KeepAlivePacket)e.Packet).KeepAliveID);
					//_log.Debug("Client received KeepAlive request, sending KeepAlive packet...");
					_protocol.SendPacket(response);
					break;
				case PacketType.LoginRequest:
					ParseLoginRequestSC((LoginRequestPacketSC) e.Packet);
					break;
				case PacketType.SpawnPosition:
					var spPack = (SpawnPositionPacket) e.Packet;
					_log.Debug(string.Format("Received SpawnPosition packet: {0}, {1}, {2}. Updating world spawn...", spPack.X, spPack.Y ,spPack.Z));
					_world.SetSpawn(spPack.X, spPack.Y, spPack.Z);
					_log.Debug("Spawn position set!");
					break;
				case PacketType.Player:
					var playerPack = (PlayerPacket) e.Packet;
					response = new PlayerPacket(playerPack.OnGround);
					_log.Debug("Updating player OnGround...");
					_player.SetOnGround(playerPack.OnGround);
					_log.Debug("Client received Player packet, responding with identical packet...");
					_protocol.SendPacket(response);
					break;
				case PacketType.PlayerPosition:
					var pPack = (PlayerPositionPacket) e.Packet;
					response = new PlayerPositionPacket(pPack.X, pPack.Y, pPack.Stance, pPack.Z, pPack.OnGround);
					_log.Debug("Updating player position...");
					_player.SetPosition(pPack.X, pPack.Y, pPack.Z);
					_player.SetStance(pPack.Stance);
					_player.SetOnGround(pPack.OnGround);
					_log.Debug("Client received PlayerPosition packet (0x0B), responding with identical packet...");
					_protocol.SendPacket(response);
					break;
				case PacketType.PlayerLook:
					var lPack = (PlayerLookPacket) e.Packet;
					response = new PlayerLookPacket(lPack.Yaw, lPack.Pitch, lPack.OnGround);
					_log.Debug("Updating player look...");
					_player.SetDirection(lPack.Yaw, lPack.Pitch);
					_player.SetOnGround(lPack.OnGround);
					_log.Debug("Client received PlayerLook packet (0x0C), responding with identical packet...");
					_protocol.SendPacket(response);
					break;
				case PacketType.PlayerPositionAndLook:
					var plPack = (PlayerPositionAndLookPacket) e.Packet;
					response = new PlayerPositionAndLookPacket
					{
						X = plPack.X,
						Y = plPack.Y,
						Z = plPack.Y,
						Stance = plPack.Stance,
						Yaw = plPack.Yaw,
						Pitch = plPack.Pitch,
						OnGround = plPack.OnGround
					};
					_log.Debug("Updating player position and look...");
					_player.SetPosition(plPack.X, plPack.Y, plPack.Z);
					_player.SetDirection(plPack.Yaw, plPack.Pitch);
					_player.SetStance(plPack.Stance);
					_player.SetOnGround(plPack.OnGround);
					_log.Debug("Client received PlayerPositionAndLook packet (0x0D), responding with identical packet...");
					_protocol.SendPacket(response);
					break;
				case PacketType.DisconnectKick:
					_log.Debug("Client DISCONNECT or KICK with reason: " + ((DisconnectKickPacket)e.Packet).Reason);
					_listener.Stop();
					break;
				default:
					//_log.Warn("Received packet: " + e.Packet.Type + " but Client is not configured to respond to this packet!");
					break;
			}
		}
	}
}
