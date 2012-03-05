/*
 * SteamFriendList.cs
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

using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Steam4NET;

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
		private readonly log4net.ILog _log;

		/// <summary>
		/// Timer used to update the friend list.
		/// </summary>
		private Timer _updateTimer;

		/// <summary>
		/// List containing all the user's friends.
		/// </summary>
		private List<SteamFriend> _list;

		/// <summary>
		/// Initialize a new <c>SteamFriendList</c>.
		/// </summary>
		internal SteamFriendList()
		{
			_log = LogManager.GetLogger(this);
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
			if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
				Thread.CurrentThread.Name = "SteamFriendUpdate";
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
			_updateTimer = null;
			_list.Clear();
			_list = null;
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
