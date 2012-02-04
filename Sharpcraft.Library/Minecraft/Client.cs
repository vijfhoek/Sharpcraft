using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

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
		public List<Item> Items { get; private set; }

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

		private void PacketReceived(object sender, PacketEventArgs e)
		{
			Packet response;
			switch (e.Packet.Type)
			{
				case PacketType.KeepAlive:
					response = new KeepAlivePacket(((KeepAlivePacket)e.Packet).KeepAliveID);
					_log.Debug("Client received KeepAlive request, sending KeepAlive packet...");
					_protocol.SendPacket(response);
					break;
				case PacketType.LoginRequest:
					_log.Debug("Client received LoginRequest packet, updating world, player and server data...");
					var lrPack = (LoginRequestPacketSC) e.Packet;
					_log.Debug("Setting Player Entity ID to " + lrPack.EntityID);
					_player.EntityID = lrPack.EntityID;
					_log.Debug("Setting map seed to " + lrPack.MapSeed);
					_world.SetSeed(lrPack.MapSeed);
					_log.Debug("Setting map type to " + lrPack.LevelType);
					_world.SetLevelType(lrPack.LevelType);
					_log.Debug("Setting server mode to " + lrPack.Gamemode);
					_server.SetMode(lrPack.Gamemode);
					_log.Debug("Setting world dimension to " + lrPack.Dimension);
					_world.SetDimension(lrPack.Dimension);
					_log.Debug("Setting world difficulty to " + lrPack.Difficulty);
					_world.SetDifficulty(lrPack.Difficulty);
					_log.Debug("Setting world height to " + lrPack.WorldHeight);
					_world.SetHeight(lrPack.WorldHeight);
					_log.Debug("Setting server max players to " + lrPack.MaxPlayers);
					if (_server.Players > lrPack.MaxPlayers)
						_server.Players = lrPack.MaxPlayers;
					_server.MaxPlayers = lrPack.MaxPlayers;
					_log.Debug("World, player and server data successfully updated!");
					break;
				case PacketType.SpawnPosition:
					var spPack = (SpawnPositionPacket) e.Packet;
					_log.Debug(string.Format("Received SpawnPosition packet: {0}, {1}, {2}. Updating world spawn...", spPack.X, spPack.Y ,spPack.Z));
					_world.SetSpawn(spPack.X, spPack.Y, spPack.Z);
					_log.Debug("Spawn position set!");
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
					_log.Debug("Client received PlayerPositionAndLook packet (0x0D), responding with identical packet...");
					_protocol.SendPacket(response);
					break;
				case PacketType.DisconnectKick:
					_log.Debug("Client DISCONNECT or KICK with reason: " + ((DisconnectKickPacket)e.Packet).Reason);
					_listener.Stop();
					break;
				default:
					_log.Info("Received packet: " + e.Packet.Type + " but Client is not configured to respond to this packet!");
					break;
			}
		}
	}
}
