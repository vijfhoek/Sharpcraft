using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharpcraft.Library.Minecraft
{
	public class Server
	{
		public string Name;
		public string Description;
		public string Address;
		private int _port;
		public int Port
		{
			get { return _port; }
			set
			{
				if (value < 0 || value > 65535)
					_port = 25566;
				else
					_port = value;
			}
		}
		private int _players;
		public int Players
		{
			get { return _players; }
			set
			{
				if (value > MaxPlayers)
					_players = MaxPlayers;
				else if (value < 0)
					_players = 0;
				else
					_players = value;
			}
		}
		private int _maxPlayers;
		public int MaxPlayers
		{
			get { return _maxPlayers; }
			set
			{
				if (value < 0)
					_maxPlayers = 0;
				else
					_maxPlayers = value;
				if (Players > _maxPlayers)
					Players = _maxPlayers;
			}
		}
		public int Ping;
		public bool Online;

		public Server(string name, string address, int port, string description = "",
			int players = 0, int maxPlayers = 0, int ping = 0, bool online = false)
		{
			Name = name;
			Description = description;
			Address = address;
			Port = port;
			MaxPlayers = maxPlayers;
			Players = players;
			Ping = ping;
			Online = online;
		}
	}
}
