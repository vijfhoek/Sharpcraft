/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Steam4NET;

using log4net;

using Sharpcraft.Logging;

namespace Sharpcraft.Steam
{
	/// <summary>
	/// Class for managing Steam friends.
	/// </summary>
	public class SteamFriendList
	{
		/// <summary>
		/// The log object for this class.
		/// </summary>
		private readonly ILog _log;

		/// <summary>
		/// Timer used to update the friend list.
		/// </summary>
		private readonly Timer _updateTimer;

		/// <summary>
		/// List containing all the user's friends.
		/// </summary>
		private readonly List<SteamFriend> _list;

		/// <summary>
		/// Initialize a new <c>SteamFriendList</c>.
		/// </summary>
		internal SteamFriendList()
		{
			_log = LoggerManager.GetLogger(this);
			_list = new List<SteamFriend>();
			_updateTimer = new Timer(Update, null, 0, 60000);
			SteamManager.OnSteamClose += SteamClose;
			_log.Info("SteamFriendList loaded!");
		}

		/// <summary>
		/// Event fired when the friend list updates.
		/// </summary>
		public event SteamFriendsEventHandler OnFriendsUpdate;
		/// <summary>
		/// Method called from <see cref="SteamFriendList" /> that fires <see cref="OnFriendsUpdate" />.
		/// </summary>
		/// <param name="e"></param>
		private void FriendsUpdate(SteamFriendsEventArgs e)
		{
			if (OnFriendsUpdate != null)
				OnFriendsUpdate(e);
		}

		/// <summary>
		/// Updates the friend list.
		/// </summary>
		/// <param name="state">N/A (Not Used)</param>
		private void Update(object state)
		{
			_log.Debug("Updating Steam friends...");
			LoadFriends();
			_log.Info("Steam friends updated!");
			FriendsUpdate(new SteamFriendsEventArgs(SteamManager.GetName(), SteamManager.GetStatus(true), _list, _list.Count, GetFriendCount(true)));
		}

		/// <summary>
		/// Handler for SteamClose event, disposes timer and clears friend list.
		/// </summary>
		private void SteamClose()
		{
			_log.Info("SteamClose event detected, disposing SteamFriendList components...");
			_updateTimer.Dispose();
			_list.Clear();
		}

		/// <summary>
		/// Load all Steam friends into list.
		/// </summary>
		private void LoadFriends()
		{
			_log.Debug("Loading Steam friends...");
			_list.Clear();
			int num = SteamManager.Friends.GetFriendCount((int) EFriendFlags.k_EFriendFlagImmediate);
			for (int i = 0; i < num; i++)
			{
				var friend = new SteamFriend(SteamManager.Friends.GetFriendByIndex(i, (int) EFriendFlags.k_EFriendFlagImmediate));
				_list.Add(friend);
				//_log.Debug("Added " + friend.GetName() + " (" + friend.GetStatus() + ")"); // Gets spammy
			}
			_log.Info("Loaded " + num + " Steam friends!");
		}

		/// <summary>
		/// Return the list of Steam friends.
		/// </summary>
		/// <returns>List of Steam friends.</returns>
		public IEnumerable<SteamFriend> GetFriends()
		{
			return _list;
		}

		/// <summary>
		/// Get number of Steam friends.
		/// </summary>
		/// <param name="online">Whether or not to only return the number of online friends.</param>
		/// <returns>Number of Steam friends.</returns>
		public int GetFriendCount(bool online = false)
		{
			return online ? _list.Count(friend => friend.GetState() == EPersonaState.k_EPersonaStateOnline) : _list.Count;
		}

		/// <summary>
		/// Get a friend by their SteamID.
		/// </summary>
		/// <param name="id"><see cref="string" /> representation of the friend's SteamID.</param>
		/// <returns>The <see cref="SteamFriend" /> found.</returns>
		public SteamFriend GetFriendBySteamId(string id)
		{
			return _list.FirstOrDefault(friend => friend.SteamID.ToString() == id);
		}

		/// <summary>
		/// Get a friend by their name (alias).
		/// </summary>
		/// <param name="name">The name (alias) to look up.</param>
		/// <returns>The <see cref="SteamFriend" /> found.</returns>
		/// <remarks>Note that the name (alias) can change frequently, do not rely on the name to keep track of friends.</remarks>
		public SteamFriend GetFriendByName(string name)
		{
			return _list.FirstOrDefault(friend => friend.GetName() == name);
		}
	}
}
