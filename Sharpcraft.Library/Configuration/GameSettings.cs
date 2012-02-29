/*
 * GameSettings.cs
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
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

using Newtonsoft.Json;

using Sharpcraft.Library.Minecraft;

namespace Sharpcraft.Library.Configuration
{
	/// <summary>
	/// Settings related to the game (Sharpcraft).
	/// </summary>
	public class GameSettings : Settings
	{
		/// <summary>
		/// Whether or not to run the game in fullscreen mode.
		/// </summary>
		public bool Fullscreen;

		private Point _size;

		/// <summary>
		/// Preferred size of the game window.
		/// </summary>
		public Point Size
		{
			get
			{
				return _size;
			}
			set
			{
				_size.X = value.X > 0 ? value.X : 1;
				_size.Y = value.Y > 0 ? value.Y : 1;
			}
		}

		/// <summary>
		/// The "Quickjoin" server.
		/// </summary>
		public Server QuickServer;

		/// <summary>
		/// Contains servers that the user has saved in the favourites list.
		/// </summary>
		[JsonProperty]
		public List<Server> Servers { get; private set; }

		/// <summary>
		/// Initializes a new instance of <c>GameSettings</c>.
		/// </summary>
		/// <param name="settingsFile">The settings file to use.</param>
		/// <param name="quickServer">The quickserver that the user last specified.</param>
		public GameSettings(string settingsFile, Server quickServer = null) : base(settingsFile)
		{
			QuickServer = quickServer;
			Servers = new List<Server>();
		}

		/// <summary>
		/// Add a server to the server list.
		/// </summary>
		/// <param name="server">The server to add.</param>
		/// <returns><c>true</c> if the server was successfully added, <c>false</c> if not (server already in list).</returns>
		public bool AddServer(Server server)
		{
			if (Servers.Any(serv => serv.Name.ToLower() == server.Name.ToLower() || (serv.Address == server.Address && serv.Port == server.Port)))
				return false;

			Servers.Add(server);
			return true;
		}

		/// <summary>
		/// Remove a server from the list by name.
		/// </summary>
		/// <param name="name">Name of the server.</param>
		/// <returns><c>true</c> if the server was removed, <c>false</c> if it was not found.</returns>
		public bool RemoveServer(string name)
		{
			for (int i = 0; i < Servers.Count; i++)
			{
				if (Servers[i].Name.ToLower() == name.ToLower())
				{
					Servers.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes a server from the list by reference.
		/// </summary>
		/// <param name="server"><see cref="Server"/> to remove.</param>
		/// <returns><c>true</c> if the server was removed, <c>false</c> if it was not found.</returns>
		public bool RemoveServer(Server server)
		{
			if (Servers.Contains(server))
			{
				Servers.Remove(server);
				return true;
			}
			return false;
		}
	}
}
