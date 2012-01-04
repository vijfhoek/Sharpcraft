using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Steam4NET;

namespace Sharpcraft.Steam
{
	public class SteamFriendList
	{
		private readonly Timer _updateTimer;

		private readonly List<SteamFriend> _list;

		internal SteamFriendList()
		{
			_list = new List<SteamFriend>();
			_updateTimer = new Timer(Update, null, 0, 60000);
		}

		public event SteamFriendsEventHandler OnFriendsUpdate;
		protected virtual void FriendsUpdate(object sender, SteamFriendsEventArgs e)
		{
			if (OnFriendsUpdate != null)
				OnFriendsUpdate(sender, e);
		}

		private void Update(object state)
		{
			Console.WriteLine("Uodate running...");
			LoadFriends();
			Console.WriteLine("Update complete!");
			FriendsUpdate(null, new SteamFriendsEventArgs(SteamManager.GetName(), SteamManager.GetStatus(true), _list, _list.Count, GetFriendCount(true)));
		}

		public void LoadFriends()
		{
			_list.Clear();
			for (int i = 0; i < SteamManager.Friends.GetFriendCount((int) EFriendFlags.k_EFriendFlagImmediate); i++)
			{
				var friend = new SteamFriend(SteamManager.Friends.GetFriendByIndex(i, (int) EFriendFlags.k_EFriendFlagImmediate));
				_list.Add(friend);
				Console.WriteLine("Added " + friend.GetName() + " (" + friend.GetStatus() + ")");
			}
		}

		public List<SteamFriend> GetFriends()
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
