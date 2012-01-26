using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sharpcraft.Networking;

namespace Sharpcraft.Library.Minecraft
{
	public class Client
	{
		private readonly Protocol _protocol;
		private readonly Server _server;
		private readonly Player _player;

		public Client(Server server)
		{
			_server = server;
			_protocol = new Protocol(_server.Address, _server.Port);
		}

		public Server GetServer()
		{
			return _server;
		}

		public bool Connect()
		{
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
