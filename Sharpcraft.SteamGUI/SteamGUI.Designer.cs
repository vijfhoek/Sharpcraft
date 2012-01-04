namespace Sharpcraft.SteamGUI
{
	partial class SteamGUI
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.friendList = new System.Windows.Forms.ListView();
			this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.userDataLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.connLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.idLabel = new System.Windows.Forms.Label();
			this.statusCaption = new System.Windows.Forms.Label();
			this.statusLabel = new System.Windows.Forms.Label();
			this.friendsCaption = new System.Windows.Forms.Label();
			this.friendsLabel = new System.Windows.Forms.Label();
			this.sendButton = new System.Windows.Forms.Button();
			this.userDataLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// friendList
			// 
			this.friendList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.statusHeader});
			this.friendList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.friendList.FullRowSelect = true;
			this.friendList.GridLines = true;
			this.friendList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.friendList.Location = new System.Drawing.Point(0, 114);
			this.friendList.Name = "friendList";
			this.friendList.Size = new System.Drawing.Size(294, 358);
			this.friendList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.friendList.TabIndex = 0;
			this.friendList.UseCompatibleStateImageBehavior = false;
			this.friendList.View = System.Windows.Forms.View.Details;
			this.friendList.SelectedIndexChanged += new System.EventHandler(this.FriendListSelectedIndexChanged);
			// 
			// nameHeader
			// 
			this.nameHeader.Text = "Name";
			this.nameHeader.Width = 180;
			// 
			// statusHeader
			// 
			this.statusHeader.Text = "Status";
			this.statusHeader.Width = 90;
			// 
			// userDataLayoutPanel
			// 
			this.userDataLayoutPanel.ColumnCount = 2;
			this.userDataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.userDataLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.userDataLayoutPanel.Controls.Add(this.connLabel, 0, 0);
			this.userDataLayoutPanel.Controls.Add(this.nameLabel, 1, 0);
			this.userDataLayoutPanel.Controls.Add(this.idLabel, 1, 1);
			this.userDataLayoutPanel.Controls.Add(this.statusCaption, 0, 2);
			this.userDataLayoutPanel.Controls.Add(this.statusLabel, 1, 2);
			this.userDataLayoutPanel.Controls.Add(this.friendsCaption, 0, 3);
			this.userDataLayoutPanel.Controls.Add(this.friendsLabel, 1, 3);
			this.userDataLayoutPanel.Controls.Add(this.sendButton, 0, 4);
			this.userDataLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.userDataLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.userDataLayoutPanel.Name = "userDataLayoutPanel";
			this.userDataLayoutPanel.RowCount = 5;
			this.userDataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.userDataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.userDataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.userDataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.userDataLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.userDataLayoutPanel.Size = new System.Drawing.Size(294, 112);
			this.userDataLayoutPanel.TabIndex = 1;
			// 
			// connLabel
			// 
			this.connLabel.AutoSize = true;
			this.connLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.connLabel.Location = new System.Drawing.Point(3, 0);
			this.connLabel.Name = "connLabel";
			this.connLabel.Size = new System.Drawing.Size(141, 20);
			this.connLabel.TabIndex = 0;
			this.connLabel.Text = "Connected as:";
			this.connLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nameLabel.Location = new System.Drawing.Point(150, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(141, 20);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "{USER_NAME}";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// idLabel
			// 
			this.idLabel.AutoSize = true;
			this.idLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.idLabel.Location = new System.Drawing.Point(150, 20);
			this.idLabel.Name = "idLabel";
			this.idLabel.Size = new System.Drawing.Size(141, 20);
			this.idLabel.TabIndex = 2;
			this.idLabel.Text = "{STEAM_ID}";
			this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// statusCaption
			// 
			this.statusCaption.AutoSize = true;
			this.statusCaption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusCaption.Location = new System.Drawing.Point(3, 40);
			this.statusCaption.Name = "statusCaption";
			this.statusCaption.Size = new System.Drawing.Size(141, 20);
			this.statusCaption.TabIndex = 3;
			this.statusCaption.Text = "Status:";
			this.statusCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Location = new System.Drawing.Point(150, 40);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(141, 20);
			this.statusLabel.TabIndex = 4;
			this.statusLabel.Text = "{STEAM_STATUS}";
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// friendsCaption
			// 
			this.friendsCaption.AutoSize = true;
			this.friendsCaption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.friendsCaption.Location = new System.Drawing.Point(3, 60);
			this.friendsCaption.Name = "friendsCaption";
			this.friendsCaption.Size = new System.Drawing.Size(141, 20);
			this.friendsCaption.TabIndex = 5;
			this.friendsCaption.Text = "Friends:";
			this.friendsCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// friendsLabel
			// 
			this.friendsLabel.AutoSize = true;
			this.friendsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.friendsLabel.Location = new System.Drawing.Point(150, 60);
			this.friendsLabel.Name = "friendsLabel";
			this.friendsLabel.Size = new System.Drawing.Size(141, 20);
			this.friendsLabel.TabIndex = 6;
			this.friendsLabel.Text = "{STEAM_FRIENDS}";
			this.friendsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// sendButton
			// 
			this.userDataLayoutPanel.SetColumnSpan(this.sendButton, 2);
			this.sendButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sendButton.Location = new System.Drawing.Point(3, 83);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(288, 26);
			this.sendButton.TabIndex = 7;
			this.sendButton.Text = "Send notice to selected friend";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.SendButtonClick);
			// 
			// SteamGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(294, 472);
			this.Controls.Add(this.userDataLayoutPanel);
			this.Controls.Add(this.friendList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "SteamGUI";
			this.ShowIcon = false;
			this.Text = "Steam GUI";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SteamGuiFormClosing);
			this.userDataLayoutPanel.ResumeLayout(false);
			this.userDataLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView friendList;
		private System.Windows.Forms.ColumnHeader nameHeader;
		private System.Windows.Forms.ColumnHeader statusHeader;
		private System.Windows.Forms.TableLayoutPanel userDataLayoutPanel;
		private System.Windows.Forms.Label connLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label idLabel;
		private System.Windows.Forms.Label statusCaption;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Label friendsCaption;
		private System.Windows.Forms.Label friendsLabel;
		private System.Windows.Forms.Button sendButton;
	}
}

