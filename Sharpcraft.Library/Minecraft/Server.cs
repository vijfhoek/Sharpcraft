using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharpcraft.Library.Minecraft
{
	/// <summary>
	/// Server object, used as reference when connecting to a server.
	/// </summary>
	public class Server
	{
		/// <summary>
		/// Name of the server, or "title".
		/// </summary>
		public string Name;

		/// <summary>
		/// Description of the server, usually broadcasted by the server itself.
		/// </summary>
		public string Description;

		/// <summary>
		/// Address of the server, usually an IP address.
		/// </summary>
		public string Address;

		/// <summary>
		/// Server port, default is 25565.
		/// </summary>
		private int _port;

		/// <summary>
		/// Server port, default is 25565.
		/// </summary>
		public int Port
		{
			get { return _port; }
			set
			{
				if (value < 0 || value > 65535)
					_port = 25565;
				else
					_port = value;
			}
		}

		/// <summary>
		/// Number of players currently playing on the server.
		/// </summary>
		private int _players;

		/// <summary>
		/// Number of players currently playing on the server.
		/// </summary>
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

		/// <summary>
		/// Max players that the server allows.
		/// </summary>
		private int _maxPlayers;

		/// <summary>
		/// Max players that the server allows.
		/// </summary>
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

		/// <summary>
		/// Ping (latency) of the server.
		/// </summary>
		public int Ping;

		/// <summary>
		/// Whether or not the server is currently online.
		/// </summary>
		public bool Online;

		/// <summary>
		/// Initialize a new <see cref="Server" />.
		/// </summary>
		/// <param name="name">Name of the server.</param>
		/// <param name="address">Address of the server.</param>
		/// <param name="port">Server's port.</param>
		/// <param name="description">Server's description.</param>
		/// <param name="players">Number of players currently on the server.</param>
		/// <param name="maxPlayers">Maximum players allowed on the server.</param>
		/// <param name="ping">Server ping.</param>
		/// <param name="online">Is the server online?</param>
		/// <remarks>name and address are the only required parameters,
		/// rest is optional and will be set to their default if omitted.</remarks>
		public Server(string name, string address, int port = 25565, string description = "",
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
