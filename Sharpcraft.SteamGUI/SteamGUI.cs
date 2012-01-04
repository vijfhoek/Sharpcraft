using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Sharpcraft.Steam;

namespace Sharpcraft.SteamGUI
{
	public partial class SteamGUI : Form
	{
		private bool _steamClosed;

		private const string FriendFormat = "{0}/{1}";
		private delegate void VoidDelegate();

		public SteamGUI()
		{
			InitializeComponent();
			_steamClosed = false;
			sendButton.Enabled = false;
			nameLabel.Text = SteamManager.GetName();
			statusLabel.Text = SteamManager.GetStatus(true);
			friendsLabel.Text = string.Format(FriendFormat, SteamManager.FriendList.GetFriendCount(true),
											  SteamManager.FriendList.GetFriendCount());

			foreach (var friend in SteamManager.FriendList.GetFriends())
			{
				var item = new ListViewItem(new[] { friend.GetName(), friend.GetStatus(true) }) { Tag = friend.SteamID };
				friendList.Items.Add(item);
			}
			SteamManager.FriendList.OnFriendsUpdate += UpdateData;
			SteamManager.OnSteamClose += SteamClose;
		}

		private void UpdateData(SteamFriendsEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke((VoidDelegate)(() => SetNewData(e.Name, e.Status, e.FriendCount, e.FriendOnlineCount, e.Friends)));
			}
			else
				SetNewData(e.Name, e.Status, e.FriendCount, e.FriendOnlineCount, e.Friends);
		}

		private void SteamClose()
		{
			if (SteamManager.FriendList != null)
				SteamManager.FriendList.OnFriendsUpdate -= UpdateData;
			_steamClosed = true;
			Close();
		}

		private void SetNewData(string name, string status, int count, int onlineCount, IEnumerable<SteamFriend> friends)
		{
			friendList.Items.Clear();
			nameLabel.Text = name;
			statusLabel.Text = status;
			friendsLabel.Text = string.Format(FriendFormat, onlineCount, count);
			foreach (var friend in friends)
			{
				var item = new ListViewItem(new[] { friend.GetName(), friend.GetStatus(true) }) { Tag = friend.SteamID };
				friendList.Items.Add(item);
			}
		}

		private void SendButtonClick(object sender, EventArgs e)
		{
			SteamManager.FriendList.GetFriendBySteamId(friendList.SelectedItems[0].Tag.ToString()).SendMessage("[Sharpcraft] This is a test message, please stay calm.");
		}

		private void FriendListSelectedIndexChanged(object sender, EventArgs e)
		{
			if (friendList.SelectedItems.Count > 0)
				sendButton.Enabled = true;
			else
				sendButton.Enabled = false;
		}

		private void SteamGuiFormClosing(object sender, FormClosingEventArgs e)
		{
			if (!_steamClosed)
				SteamManager.FriendList.OnFriendsUpdate -= UpdateData;
		}
	}
}
