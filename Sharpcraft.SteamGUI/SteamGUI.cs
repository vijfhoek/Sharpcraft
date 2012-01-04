/* 
 * Sharpcraft.SteamGUI
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Sharpcraft.Steam;
using Sharpcraft.Logging;

using log4net;

namespace Sharpcraft.SteamGUI
{
	public partial class SteamGUI : Form
	{
		private ILog _log;

		private bool _steamClosed;

		private const string FriendFormat = "{0}/{1}";
		private delegate void VoidDelegate();

		public SteamGUI()
		{
			InitializeComponent();
			_log = LoggerManager.GetLogger(this);
			_log.Info("SteamGUI is initializing...");
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
			_log.Debug("SteamGUI is registering to OnSteamClose event...");
			SteamManager.OnSteamClose += SteamClose;
			_log.Info("SteamGUI loaded!");
		}

		private void UpdateData(SteamFriendsEventArgs e)
		{
			if (InvokeRequired)
				Invoke((VoidDelegate)(() => SetNewData(e.Name, e.Status, e.FriendCount, e.FriendOnlineCount, e.Friends)));
			else
				SetNewData(e.Name, e.Status, e.FriendCount, e.FriendOnlineCount, e.Friends);
		}

		private void SteamClose()
		{
			_log.Info("[SteamClose] Closing SteamGUI...");
			if (SteamManager.FriendList != null)
				SteamManager.FriendList.OnFriendsUpdate -= UpdateData;
			_steamClosed = true;
			if (InvokeRequired)
				Invoke((VoidDelegate) (Close));
			else
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
			sendButton.Enabled = friendList.SelectedItems.Count > 0;
		}

		private void SteamGuiFormClosing(object sender, FormClosingEventArgs e)
		{
			_log.Debug("SteamGUI is closing!");
			if (!_steamClosed)
				SteamManager.FriendList.OnFriendsUpdate -= UpdateData;
		}
	}
}
