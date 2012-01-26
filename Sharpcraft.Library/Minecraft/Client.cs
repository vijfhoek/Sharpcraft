using System;
using System.Collections.Generic;

using Sharpcraft.Logging;
using Sharpcraft.Networking;

namespace Sharpcraft.Library.Minecraft
{
	public class Client
	{
		private readonly log4net.ILog _log;

		private readonly Protocol _protocol;
		private readonly Server _server;
		private readonly Player _player;

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
			_protocol.SendPacket(new LoginRequestPacketCS(22, _player.Name));
			_log.Info("Waiting for login response...");
			response = _protocol.GetPacket();
			if (!(response is LoginRequestPacketSC))
			{
				_log.Warn("Incorrect packet type sent as response! Expected LoginRequestPacketSC, got " + response.GetType());
				return false;
			}
			_log.Info("Server responded to login with mapseed " + ((LoginRequestPacketSC)response).MapSeed);
			_log.Info("Further connection methods not yet implemented, halting Connect!");
			return true;
		}

		public bool Disconnect()
		{
			return false; // For now
		}

		public void SendMessage(string message)
		{
			
		}

		public void SendEmote(string emote)
		{
			
		}

		public void SendCommand(string command)
		{
			
		}
	}
}
