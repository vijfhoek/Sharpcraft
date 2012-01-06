/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Collections.Generic;

namespace Sharpcraft.Steam
{
	/// <summary>
	/// <c>SteamFriendsEventArgs</c> class used to communicate friend updates to listeners.
	/// </summary>
	public class SteamFriendsEventArgs : EventArgs
	{
		/// <summary>
		/// The name of the currently logged in user.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// String representation of the current user's status.
		/// </summary>
		public string Status { get; private set; }

		/// <summary>
		/// List of the user's friends.
		/// </summary>
		public List<SteamFriend> Friends { get; private set; }
		/// <summary>
		/// Total number of friends.
		/// </summary>
		public int FriendCount { get; private set; }
		/// <summary>
		/// Number of friends who are currently online.
		/// </summary>
		public int FriendOnlineCount { get; private set; }

		/// <summary>
		/// Initialize a new instance of <c>SteamFriendsEventArgs</c>.
		/// </summary>
		/// <param name="name">Current name of logged in user.</param>
		/// <param name="status">String representation of the user's status.</param>
		/// <param name="friends">List of user's friends.</param>
		/// <param name="count">Total number of friends.</param>
		/// <param name="onlineCount">Number of friends who are currently online.</param>
		internal SteamFriendsEventArgs(string name, string status, List<SteamFriend> friends, int count, int onlineCount)
		{
			Name = name;
			Status = status;
			Friends = friends;
			FriendCount = count;
			FriendOnlineCount = onlineCount;
		}
	}

	
	public delegate void SteamCloseEventHandler();
	public delegate void SteamFriendsEventHandler(SteamFriendsEventArgs e);
}
