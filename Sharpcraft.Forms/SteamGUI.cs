/*
 * SteamGUI.cs
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
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Sharpcraft.Steam;
using Sharpcraft.Logging;

namespace Sharpcraft.Forms
{
	/// <summary>
	/// GUI for <see cref="Steam" /> with friend list and a send message button.
	/// </summary>
	public partial class SteamGUI : Form
	{
		/// <summary>
		/// Logger object for this class.
		/// </summary>
		private readonly log4net.ILog _log;

		/// <summary>
		/// Bool indicating whether or not the Steam client has closed.
		/// </summary>
		private bool _steamClosed;

		/// <summary>
		/// String format used for displaying friend cound.
		/// &lt;number of online friends&gt;/&lt;total number of friends&gt;
		/// </summary>
		private const string FriendFormat = "{0}/{1}";
		/// <summary>
		/// Empty delegate used for invoking.
		/// </summary>
		private delegate void VoidDelegate();

		/// <summary>
		/// Initializes a new Steam GUI.
		/// </summary>
		public SteamGUI()
		{
			InitializeComponent();
			_log = LogManager.GetLogger(this);
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

		/// <summary>
		/// Update the friend list with new data.
		/// </summary>
		/// <param name="e"><see cref="SteamFriendsEventArgs" /> containing the new data.</param>
		/// <remarks>This method is used as event handler, normal updating should be done with the <see cref="SetNewData" /> method.</remarks>
		private void UpdateData(SteamFriendsEventArgs e)
		{
			if (InvokeRequired)
				Invoke((VoidDelegate)(() => SetNewData(e.Name, e.Status, e.FriendCount, e.FriendOnlineCount, e.Friends)));
			else
				SetNewData(e.Name, e.Status, e.FriendCount, e.FriendOnlineCount, e.Friends);
		}

		/// <summary>
		/// Handler for SteamClose event.
		/// </summary>
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

		/// <summary>
		/// Updates the friend list with new data.
		/// </summary>
		/// <param name="name">Name of the currently logged in user.</param>
		/// <param name="status">Status of the currently logged in user.</param>
		/// <param name="count">Number of friends.</param>
		/// <param name="onlineCount">Number of online friends.</param>
		/// <param name="friends">IEnumerable containing all friends as instances of <see cref="SteamFriend" />.</param>
		/// <remarks>The name of the currently logged in user might change frequently, that's why it needs to be refreshed.</remarks>
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

		/// <summary>
		/// SendButton click handler. Sends a message to currently selected Steam friend with Minecraft server info.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void SendButtonClick(object sender, EventArgs e)
		{
			SteamManager.FriendList.GetFriendBySteamId(friendList.SelectedItems[0].Tag.ToString()).SendMessage("[Sharpcraft] This is a test message, please stay calm.");
		}

		/// <summary>
		/// FriendList SelectedIndexChanged handler, verifies valid selection and that selected friend is online.
		/// If validation is successful, the send button is enabled, otherwise it is disabled.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void FriendListSelectedIndexChanged(object sender, EventArgs e)
		{
			bool validSelection = friendList.SelectedItems.Count > 0;
			SteamFriend friend = validSelection ? SteamManager.FriendList.GetFriendBySteamId(friendList.SelectedItems[0].Tag.ToString()) : null;
			bool validFriend = friend != null && friend.IsOnline();
			if (validSelection && validFriend)
				sendButton.Enabled = true;
			else
				sendButton.Enabled = false;
		}

		/// <summary>
		/// SteamGUI FormClosing handler, unregisters all registered events.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void SteamGuiFormClosing(object sender, FormClosingEventArgs e)
		{
			_log.Debug("SteamGUI is closing!");
			if (!_steamClosed)
				SteamManager.FriendList.OnFriendsUpdate -= UpdateData;
		}

		/// <summary>
		/// SteamGUI FormClosed handler, logs to logger that form has closed.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void SteamGuiFormClosed(object sender, FormClosedEventArgs e)
		{
			_log.Info("SteamGUI has closed: " + e.CloseReason);
		}
	}
}
