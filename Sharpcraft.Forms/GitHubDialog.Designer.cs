namespace Sharpcraft.Forms
{
	partial class GitHubDialog
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
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.CloseButton = new System.Windows.Forms.Button();
			this.ClipboardButton = new System.Windows.Forms.Button();
			this.IssueBox = new System.Windows.Forms.TextBox();
			this.ButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.DescriptionLabel.Location = new System.Drawing.Point(0, 0);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(584, 28);
			this.DescriptionLabel.TabIndex = 0;
			this.DescriptionLabel.Text = "Copy this text into your GitHub issue and please include more information about t" +
    "he problem (such as what you were doing when it happened).";
			this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Controls.Add(this.StatusLabel);
			this.ButtonPanel.Controls.Add(this.CloseButton);
			this.ButtonPanel.Controls.Add(this.ClipboardButton);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ButtonPanel.Location = new System.Drawing.Point(0, 354);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(584, 30);
			this.ButtonPanel.TabIndex = 1;
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.StatusLabel.Location = new System.Drawing.Point(177, 3);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(230, 23);
			this.StatusLabel.TabIndex = 2;
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseButton.Location = new System.Drawing.Point(413, 3);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(165, 23);
			this.CloseButton.TabIndex = 1;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
			// 
			// ClipboardButton
			// 
			this.ClipboardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ClipboardButton.Location = new System.Drawing.Point(6, 3);
			this.ClipboardButton.Name = "ClipboardButton";
			this.ClipboardButton.Size = new System.Drawing.Size(165, 23);
			this.ClipboardButton.TabIndex = 0;
			this.ClipboardButton.Text = "Copy contents to clipboard";
			this.ClipboardButton.UseVisualStyleBackColor = true;
			this.ClipboardButton.Click += new System.EventHandler(this.ClipboardButtonClick);
			// 
			// IssueBox
			// 
			this.IssueBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.IssueBox.Location = new System.Drawing.Point(0, 28);
			this.IssueBox.Multiline = true;
			this.IssueBox.Name = "IssueBox";
			this.IssueBox.ReadOnly = true;
			this.IssueBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.IssueBox.Size = new System.Drawing.Size(584, 326);
			this.IssueBox.TabIndex = 2;
			this.IssueBox.Click += new System.EventHandler(this.IssueBoxClick);
			this.IssueBox.Enter += new System.EventHandler(this.IssueBoxEnter);
			// 
			// GitHubDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 384);
			this.ControlBox = false;
			this.Controls.Add(this.IssueBox);
			this.Controls.Add(this.ButtonPanel);
			this.Controls.Add(this.DescriptionLabel);
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "GitHubDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GitHub Issue Help";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.GitHubDialogShown);
			this.ButtonPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.Panel ButtonPanel;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Button ClipboardButton;
		private System.Windows.Forms.TextBox IssueBox;
		private System.Windows.Forms.Label StatusLabel;
	}
}