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

		public Client(Server server)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("Minecraft Client created!");
			_server = server;
			_log.Debug("Creating communication protocol...");
			_protocol = new Protocol(_server.Address, _server.Port);
			_log.Info("Client initiated on " + _server.Address + ":" + _server.Port + "!");
		}

		public Server GetServer()
		{
			return _server;
		}

		public bool Connect()
		{
			_log.Info("Connecting to " + _server.Address + ":" + _server.Port + "...");
			_protocol.SendPacket(new HandshakePacketCS(_player.Name));
			return false; // For now
		}

		public bool Disconnect()
		{
			return false;
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
