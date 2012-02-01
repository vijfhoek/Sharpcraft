using System;
using System.Collections.Generic;

using Sharpcraft.Logging;
using Sharpcraft.Networking;
using Sharpcraft.Networking.Enums;
using Sharpcraft.Networking.Packets;

namespace Sharpcraft.Library.Minecraft
{
	public class Client
	{
		private readonly log4net.ILog _log;

		private readonly Protocol _protocol;
		private readonly Server _server;
		private readonly Player _player;

		private PacketListener _listener;

		public Client(Server server, Player player)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("Minecraft Client created!");
			_server = server;
			_log.Debug("Creating communication protocol...");
			_protocol = new Protocol(_server.Address, _server.Port);
			_log.Info("Client initiated on " + _server.Address + ":" + _server.Port + "!");
			_player = player;
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
			_protocol.SendPacket(new LoginRequestPacketCS(Constants.ProtocolVersion, _player.Name));
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
				case PacketType.DisconnectKick:
					_log.Debug("Client DISCONNECT or KICK with reason: " + ((DisconnectKickPacket)e.Packet).Reason);
					break;
				default:
					_log.Info("Received packet: " + e.Packet.Type + " but Client is not configured to respond to this packet!");
					break;
			}
		}
	}
}
