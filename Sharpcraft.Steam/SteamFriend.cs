/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
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
