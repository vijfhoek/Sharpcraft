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
	public class SteamFriendList
	{
		private readonly ILog _log;

		private readonly Timer _updateTimer;

		private readonly List<SteamFriend> _list;

		internal SteamFriendList()
		{
			_log = LoggerManager.GetLogger(this);
			_list = new List<SteamFriend>();
			_updateTimer = new Timer(Update, null, 0, 60000);
			SteamManager.OnSteamClose += SteamClose;
			_log.Info("SteamFriendList loaded!");
		}

		public event SteamFriendsEventHandler OnFriendsUpdate;
		private void FriendsUpdate(SteamFriendsEventArgs e)
		{
			if (OnFriendsUpdate != null)
				OnFriendsUpdate(e);
		}

		private void Update(object state)
		{
			_log.Debug("Updating Steam friends...");
			LoadFriends();
			_log.Info("Steam friends updated!");
			FriendsUpdate(new SteamFriendsEventArgs(SteamManager.GetName(), SteamManager.GetStatus(true), _list, _list.Count, GetFriendCount(true)));
		}

		private void SteamClose()
		{
			_log.Info("SteamClose event detected, disposing SteamFriendList components...");
			_updateTimer.Dispose();
			_list.Clear();
		}

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

		public IEnumerable<SteamFriend> GetFriends()
		{
			return _list;
		}

		public int GetFriendCount(bool online = false)
		{
			return online ? _list.Count(friend => friend.GetState() == EPersonaState.k_EPersonaStateOnline) : _list.Count;
		}

		public SteamFriend GetFriendBySteamId(string id)
		{
			return _list.FirstOrDefault(friend => friend.SteamID.ToString() == id);
		}

		public SteamFriend GetFriendByName(string name)
		{
			return _list.FirstOrDefault(friend => friend.GetName() == name);
		}
	}
}
