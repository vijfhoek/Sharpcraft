using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharpcraft.Steam
{
	public class SteamFriendsEventArgs : EventArgs
	{
		public string Name { get; private set; }
		public string Status { get; private set; }

		public List<SteamFriend> Friends { get; private set; }
		public int FriendCount { get; private set; }
		public int FriendOnlineCount { get; private set; }

		internal SteamFriendsEventArgs(string name, string status, List<SteamFriend> friends, int count, int onlineCount)
		{
			Name = name;
			Status = status;
			Friends = friends;
			FriendCount = count;
			FriendOnlineCount = onlineCount;
		}
	}

	public delegate void SteamFriendsEventHandler(object sender, SteamFriendsEventArgs e);
}
