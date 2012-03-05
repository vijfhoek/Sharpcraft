/*
 * Events.cs
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

using System;
using Sharpcraft.Networking.Packets;

namespace Sharpcraft.Networking
{
	/// <summary>
	/// The <c>LoginEventArgs</c> class used for the login event.
	/// </summary>
	public class LoginEventArgs : EventArgs
	{
		/// <summary>
		/// The <see cref="LoginResult" /> of the login.
		/// </summary>
		public LoginResult Result;

		/// <summary>
		/// Initializes a new instance of the <c>LoginEventArgs</c> class.
		/// </summary>
		/// <param name="result"></param>
		internal LoginEventArgs(LoginResult result)
		{
			Result = result;
		}
	}

	/// <summary>
	/// The <c>PacketEventArgs</c> class used for the packet (received) event.
	/// </summary>
	public class PacketEventArgs : EventArgs
	{
		/// <summary>
		/// The <see cref="Packet" /> that was received.
		/// </summary>
		public Packet Packet { get; private set; }

		/// <summary>
		/// Initialize a new instance of <see cref="PacketEventArgs" />.
		/// </summary>
		/// <param name="packet"></param>
		internal PacketEventArgs(Packet packet)
		{
			Packet = packet;
		}
	}

	/// <summary>
	/// The login event handler.
	/// </summary>
	/// <param name="e">The <see cref="LoginEventArgs" /> associated with this event.</param>
	public delegate void LoginEventHandler(LoginEventArgs e);

	/// <summary>
	/// The packet event handler.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="eventArgs">The <see cref="Packet" /> that was received.</param>
	public delegate void PacketEventHandler(object sender, PacketEventArgs eventArgs);
}
