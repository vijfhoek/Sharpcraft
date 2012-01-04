using System;
using System.Linq;
using System.Collections.Generic;

using Steam4NET;

namespace Sharpcraft.Steam
{
	public class SteamFriendList
	{
		private readonly List<SteamFriend> _list;

		internal SteamFriendList()
		{
			_list = new List<SteamFriend>();
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
