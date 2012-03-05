/*
 * User.cs
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

namespace Sharpcraft
{
	/// <summary>
	/// Represents the currently logged in minecraft user.
	/// </summary>
	public class User
	{
		/// <summary>
		/// The session ID returned by the minecraft servers at login.
		/// </summary>
		public string SessionID { get; private set; }

		/// <summary>
		/// The name of this user.
		/// </summary>
		private string _name;

		/// <summary>
		/// Initialize a new instance of the <c>User</c> class.
		/// </summary>
		/// <param name="name">Name of the user.</param>
		/// <param name="sessionId">Session ID for this user.</param>
		/// <remarks>All of the parameters are optional and can be set later.</remarks>
		internal User(string name = null, string sessionId = null)
		{
			_name = name;
			SessionID = sessionId;
		}

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <param name="pretty"><c>true</c> to capitalize the first letter and make the rest lowercase, <c>false</c> for unmodified.</param>
		/// <returns>The user's name.</returns>
		public string GetName(bool pretty = false)
		{
			if (pretty)
			{
				char[] a = _name.ToCharArray();
				a[0] = char.ToUpper(a[0]);
				return new string(a);
			}
			return _name;
		}

		/// <summary>
		/// Set the name of the user.
		/// </summary>
		/// <param name="name">The new name to set.</param>
		/// <remarks>Note that this is entirely client-side,
		/// this does not update the actual username on the minecraft servers.</remarks>
		public void SetName(string name)
		{
			_name = name;
		}

		/// <summary>
		/// Set this user's session ID.
		/// </summary>
		/// <param name="id">The session ID to set.</param>
		public void SetSessionID(string id)
		{
			SessionID = id;
		}
	}
}
