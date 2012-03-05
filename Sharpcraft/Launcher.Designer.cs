/*
 * Launcher.Designer.cs
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

namespace Sharpcraft
{
	partial class Launcher
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
			this.InterfacePanel = new System.Windows.Forms.Panel();
			this.NewsPanel = new System.Windows.Forms.Panel();
			this.NewsLabel = new System.Windows.Forms.Label();
			this.LoginPanel = new System.Windows.Forms.Panel();
			this.LoginLabel = new System.Windows.Forms.Label();
			this.PassBox = new System.Windows.Forms.TextBox();
			this.UserBox = new System.Windows.Forms.TextBox();
			this.RememberCheckbox = new System.Windows.Forms.CheckBox();
			this.RegisterLink = new System.Windows.Forms.LinkLabel();
			this.LoginButton = new System.Windows.Forms.Button();
			this.VersionLabel = new System.Windows.Forms.Label();
			this.InterfacePanel.SuspendLayout();
			this.NewsPanel.SuspendLayout();
			this.LoginPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// InterfacePanel
			// 
			this.InterfacePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.InterfacePanel.Controls.Add(this.NewsPanel);
			this.InterfacePanel.Controls.Add(this.LoginPanel);
			this.InterfacePanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.InterfacePanel.Location = new System.Drawing.Point(538, 0);
			this.InterfacePanel.Name = "InterfacePanel";
			this.InterfacePanel.Size = new System.Drawing.Size(256, 472);
			this.InterfacePanel.TabIndex = 0;
			// 
			// NewsPanel
			// 
			this.NewsPanel.BackColor = System.Drawing.Color.Transparent;
			this.NewsPanel.Controls.Add(this.NewsLabel);
			this.NewsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.NewsPanel.Location = new System.Drawing.Point(0, 0);
			this.NewsPanel.Name = "NewsPanel";
			this.NewsPanel.Size = new System.Drawing.Size(256, 342);
			this.NewsPanel.TabIndex = 0;
			// 
			// NewsLabel
			// 
			this.NewsLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.NewsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.NewsLabel.Location = new System.Drawing.Point(0, 0);
			this.NewsLabel.Name = "NewsLabel";
			this.NewsLabel.Size = new System.Drawing.Size(256, 25);
			this.NewsLabel.TabIndex = 0;
			this.NewsLabel.Text = "Sharpcraft News";
			this.NewsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LoginPanel
			// 
			this.LoginPanel.BackColor = System.Drawing.Color.Transparent;
			this.LoginPanel.Controls.Add(this.LoginLabel);
			this.LoginPanel.Controls.Add(this.PassBox);
			this.LoginPanel.Controls.Add(this.UserBox);
			this.LoginPanel.Controls.Add(this.RememberCheckbox);
			this.LoginPanel.Controls.Add(this.RegisterLink);
			this.LoginPanel.Controls.Add(this.LoginButton);
			this.LoginPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LoginPanel.Location = new System.Drawing.Point(0, 342);
			this.LoginPanel.Name = "LoginPanel";
			this.LoginPanel.Size = new System.Drawing.Size(256, 130);
			this.LoginPanel.TabIndex = 1;
			// 
			// LoginLabel
			// 
			this.LoginLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.LoginLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.LoginLabel.Location = new System.Drawing.Point(8, 9);
			this.LoginLabel.Name = "LoginLabel";
			this.LoginLabel.Size = new System.Drawing.Size(244, 14);
			this.LoginLabel.TabIndex = 0;
			this.LoginLabel.Text = "Login with your Minecraft username and password";
			// 
			// PassBox
			// 
			this.PassBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PassBox.ForeColor = System.Drawing.Color.DimGray;
			this.PassBox.Location = new System.Drawing.Point(55, 54);
			this.PassBox.Name = "PassBox";
			this.PassBox.PasswordChar = '*';
			this.PassBox.Size = new System.Drawing.Size(150, 20);
			this.PassBox.TabIndex = 2;
			this.PassBox.Text = "Password";
			this.PassBox.TextChanged += new System.EventHandler(this.PassBoxTextChanged);
			this.PassBox.Enter += new System.EventHandler(this.PassBoxEnter);
			this.PassBox.Leave += new System.EventHandler(this.PassBoxLeave);
			// 
			// UserBox
			// 
			this.UserBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UserBox.ForeColor = System.Drawing.Color.DimGray;
			this.UserBox.Location = new System.Drawing.Point(55, 29);
			this.UserBox.Name = "UserBox";
			this.UserBox.Size = new System.Drawing.Size(150, 20);
			this.UserBox.TabIndex = 1;
			this.UserBox.Text = "Username";
			this.UserBox.TextChanged += new System.EventHandler(this.UserBoxTextChanged);
			this.UserBox.Enter += new System.EventHandler(this.UserBoxEnter);
			this.UserBox.Leave += new System.EventHandler(this.UserBoxLeave);
			// 
			// RememberCheckbox
			// 
			this.RememberCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RememberCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.RememberCheckbox.Location = new System.Drawing.Point(59, 80);
			this.RememberCheckbox.Name = "RememberCheckbox";
			this.RememberCheckbox.Size = new System.Drawing.Size(142, 17);
			this.RememberCheckbox.TabIndex = 3;
			this.RememberCheckbox.Text = "Remember my password";
			this.RememberCheckbox.UseVisualStyleBackColor = true;
			// 
			// RegisterLink
			// 
			this.RegisterLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.RegisterLink.AutoSize = true;
			this.RegisterLink.Location = new System.Drawing.Point(192, 105);
			this.RegisterLink.Name = "RegisterLink";
			this.RegisterLink.Size = new System.Drawing.Size(46, 13);
			this.RegisterLink.TabIndex = 5;
			this.RegisterLink.TabStop = true;
			this.RegisterLink.Text = "Register";
			this.RegisterLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RegisterLinkLinkClicked);
			// 
			// LoginButton
			// 
			this.LoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.LoginButton.Enabled = false;
			this.LoginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LoginButton.Location = new System.Drawing.Point(80, 99);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(100, 25);
			this.LoginButton.TabIndex = 4;
			this.LoginButton.Text = "Login";
			this.LoginButton.UseVisualStyleBackColor = true;
			this.LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
			// 
			// VersionLabel
			// 
			this.VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.VersionLabel.AutoSize = true;
			this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
			this.VersionLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.VersionLabel.ForeColor = System.Drawing.Color.White;
			this.VersionLabel.Location = new System.Drawing.Point(6, 450);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(113, 13);
			this.VersionLabel.TabIndex = 1;
			this.VersionLabel.Text = "Version {0} ({1}) by {2}";
			this.VersionLabel.Click += new System.EventHandler(this.VersionLabelClick);
			// 
			// Launcher
			// 
			this.AcceptButton = this.LoginButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size(794, 472);
			this.Controls.Add(this.VersionLabel);
			this.Controls.Add(this.InterfacePanel);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Launcher";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sharpcraft Launcher";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LauncherFormClosed);
			this.InterfacePanel.ResumeLayout(false);
			this.NewsPanel.ResumeLayout(false);
			this.LoginPanel.ResumeLayout(false);
			this.LoginPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel InterfacePanel;
		private System.Windows.Forms.Panel NewsPanel;
		private System.Windows.Forms.Label NewsLabel;
		private System.Windows.Forms.Panel LoginPanel;
		private System.Windows.Forms.Label LoginLabel;
		private System.Windows.Forms.TextBox PassBox;
		private System.Windows.Forms.TextBox UserBox;
		private System.Windows.Forms.CheckBox RememberCheckbox;
		private System.Windows.Forms.LinkLabel RegisterLink;
		private System.Windows.Forms.Button LoginButton;
		private System.Windows.Forms.Label VersionLabel;
	}
}