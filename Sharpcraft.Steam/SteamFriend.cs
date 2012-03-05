/*
 * SteamFriend.cs
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

using Steam4NET;

namespace Sharpcraft.Steam
{
	/// <summary>
	/// Represents a steam user that is a friend of the currently logged in user.
	/// </summary>
	public class SteamFriend
	{
		/// <summary>
		/// SteamID associated with this friend.
		/// </summary>
		public readonly CSteamID SteamID;
		
		/// <summary>
		/// Initialize a new <c>SteamFriend</c>.
		/// </summary>
		/// <param name="steamID"><see cref="CSteamID" /> of this friend.</param>
		internal SteamFriend(CSteamID steamID)
		{
			SteamID = steamID;
		}

		/// <summary>
		/// Gets the current name of this friend.
		/// </summary>
		/// <returns>Friend's name as a <see cref="string" />.</returns>
		public string GetName()
		{
			return SteamManager.Friends.GetFriendPersonaName(SteamID);
		}

		/// <summary>
		/// Get the <see cref="EPersonaState" /> of this friend.
		/// </summary>
		/// <returns>An <see cref="EPersonaState" /> of this friend.</returns>
		public EPersonaState GetState()
		{
			return SteamManager.Friends.GetFriendPersonaState(SteamID);
		}

		/// <summary>
		/// Get a <see cref="string" /> representation of this friend's state.
		/// </summary>
		/// <param name="pretty">If <c>true</c>, will change the format of the return value to have the first letter capitalized.</param>
		/// <returns><see cref="string" /> representation of this friend's state.</returns>
		public string GetStatus(bool pretty = false)
		{
			return SteamUtils.StateToStatus(GetState(), pretty);
		}

		/// <summary>
		/// Check if this friend is online-
		/// </summary>
		/// <returns><c>true</c> if friend is online, <c>false</c> otherwise.</returns>
		public bool IsOnline()
		{
			return GetState() == EPersonaState.k_EPersonaStateOnline;
		}

		/// <summary>
		/// Send a message to this friend.
		/// </summary>
		/// <param name="message">Message to send.</param>
		public void SendMessage(string message)
		{
			byte[] msg = SteamUtils.StringToByte(message);
			SteamManager.Friends.SendMsgToFriend(SteamID, EChatEntryType.k_EChatEntryTypeChatMsg, msg, msg.Length);
		}

		/// <summary>
		/// Send an emote to this friend.
		/// </summary>
		/// <param name="emote">Emote to send.</param>
		/// <remarks>This will appear in the target's chatlog as "&lt;user&gt; &lt;emote&gt;.</remarks>
		public void SendEmote(string emote)
		{
			byte[] em = SteamUtils.StringToByte(emote);
			SteamManager.Friends.SendMsgToFriend(SteamID, EChatEntryType.k_EChatEntryTypeEmote, em, em.Length);
		}
	}
}
