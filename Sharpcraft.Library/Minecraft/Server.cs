/*
 * Server.cs
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
		private ushort _port;

		/// <summary>
		/// Server port, default is 25565.
		/// </summary>
		public ushort Port
		{
			get { return _port; }
			set
			{
				if (value < 0 || value > ushort.MaxValue)
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
				_maxPlayers = value < 0 ? 0 : value;
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
		/// Mode of the server.
		/// 0 = Survival, 1 = Creative.
		/// </summary>
		public int Mode { get; private set; }

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
		/// <param name="mode">The mode of the server (0 = Survival, 1 = Creative).</param>
		/// <remarks>name and address are the only required parameters,
		/// rest is optional and will be set to their default if omitted.</remarks>
		public Server(string name, string address, ushort port = 25565, string description = "",
			int players = 0, int maxPlayers = 0, int ping = 0, bool online = false, int mode = 0)
		{
			Name = name;
			Description = description;
			Address = address;
			Port = port;
			MaxPlayers = maxPlayers;
			Players = players;
			Ping = ping;
			Online = online;
			Mode = mode;
		}

		/// <summary>
		/// Set the mode of this server.
		/// </summary>
		/// <param name="mode">Mode to set.</param>
		public void SetMode(int mode)
		{
			if (mode < 0 || mode > 1)
				return;
			Mode = mode;
		}
	}
}
