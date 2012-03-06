/*
 * Client.cs
 * 
 * Copyright © 2011-2012 by Sijmen Schoon and Adam Hellberg.
 * 
 * This file is part of Sharpcraft.
 * 
 * Sharpcraft is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Sharpcraft is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Sharpcraft.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Disclaimer: Sharpcraft is in no way affiliated with Mojang AB and/or
 * any of its employees and/or licensors.
 * Sijmen Schoon and Adam Hellberg do not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

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
	/// <summary>
	/// The Minecraft client.
	/// </summary>
	public class Client
	{
		private const string HandshakeFormat = "{0};{1}:{2}";

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

		/// <summary>
		/// Initialize a new Minecraft client.
		/// </summary>
		/// <param name="server">The server to connect to.</param>
		/// <param name="player">The player who logged in with the client.</param>
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

		/// <summary>
		/// Get an item by its ID.
		/// </summary>
		/// <param name="id">ID of the item.</param>
		/// <returns>The item matching the ID.</returns>
		public static Item GetItemByID(short id)
		{
			return Items.Where(item => item.Id == id).FirstOrDefault();
		}

		/// <summary>
		/// Get the server this client is currently connected to.
		/// </summary>
		/// <returns>The <see cref="Server"/> object of the current server.</returns>
		public Server GetServer()
		{
			return _server;
		}

		/// <summary>
		/// Attempt to connect to the server.
		/// </summary>
		/// <returns><c>true</c> if connection succeeded, <c>false</c> otherwise.</returns>
		public bool Connect()
		{
			// We need to create a connection thread
			_log.Info("Connecting to " + _server.Address + ":" + _server.Port + "...");
			_protocol.SendPacket(new HandshakePacketCS(string.Format(HandshakeFormat, _player.Name, _server.Address, _server.Port)));
			_log.Info("Waiting for handshake response...");
			Packet response = _protocol.GetPacket();
			if (!(response is HandshakePacketSC))
			{
				_log.Warn("Incorrect packet type sent as response! Expected HandshakePacketSC, got " + response.GetType());
				if (response is DisconnectKickPacket) _log.Warn("Kick message: " + ((DisconnectKickPacket)response).Reason);
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
			ParseLoginRequestSC((LoginRequestPacketSC) response);
			_log.Info("Creating packet listener...");
			_listener = new PacketListener(_protocol);
			_listener.OnPacketReceived += PacketReceived;
			_log.Info("Further connection methods not yet implemented, halting Connect!");
			return true;
		}

		/// <summary>
		/// Disconnect from current server.
		/// </summary>
		/// <returns><c>true</c> if disconnect was successful and clean, <c>false</c> otherwise.</returns>
		public bool Disconnect()
		{
			return false; // For now
		}

		/// <summary>
		/// Exit client.
		/// </summary>
		public void Exit()
		{
			Disconnect();
			try
			{
				_listener.OnPacketReceived -= PacketReceived;
				_listener.Stop();
			}
			catch
			{
			}
		}

		/// <summary>
		/// Send a chat message to the server.
		/// </summary>
		/// <param name="message">Message to send.</param>
		public void SendChatMessage(string message)
		{
			_protocol.SendPacket(new ChatMessagePacket(message));
		}

		/// <summary>
		/// Perform an emote.
		/// </summary>
		/// <param name="emote">Emote to perform.</param>
		/// <remarks>Displays as "&lt;player&gt; &lt;emote&gt;.</remarks>
		public void SendEmote(string emote)
		{

		}

		/// <summary>
		/// Send a command to the server.
		/// </summary>
		/// <param name="command">Command to send.</param>
		public void SendCommand(string command)
		{
			//NOTE(F16Gaming): Is this actually needed? Or does the server parse / commands automatically?
			//NOTE(Vijfhoek): It does AFAIK
			SendChatMessage(command);
		}

		/// <summary>
		/// Get the player associated with this <see cref="Client" />.
		/// </summary>
		/// <returns>The <see cref="Player" /> object associated with this <see cref="Client" />.</returns>
		public Player GetPlayer()
		{
			return _player;
		}

		/// <summary>
		/// Parse a <see cref="LoginRequestPacketSC" /> packet and update data accordingly.
		/// </summary>
		/// <param name="packet">The <see cref="LoginRequestPacketSC" /> to parse.</param>
		private void ParseLoginRequestSC(LoginRequestPacketSC packet)
		{
			_log.Debug("Updating world, player and server data...");
			_log.Debug("Setting Player Entity ID to " + packet.EntityID);
			_player.EntityID = packet.EntityID;
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

		/// <summary>
		/// Handles all packets received.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e"><see cref="PacketEventArgs" /> containing the packet and other info.</param>
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
