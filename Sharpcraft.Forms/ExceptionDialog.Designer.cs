namespace Sharpcraft.Forms
{
	partial class ExceptionDialog
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
			this.ExceptionLabel = new System.Windows.Forms.Label();
			this.MessageGroup = new System.Windows.Forms.GroupBox();
			this.ExceptionMessage = new System.Windows.Forms.TextBox();
			this.StackTraceGroup = new System.Windows.Forms.GroupBox();
			this.ExceptionStackTrace = new System.Windows.Forms.TextBox();
			this.SendButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.ExtraInfoCheckbox = new System.Windows.Forms.CheckBox();
			this.MessageGroup.SuspendLayout();
			this.StackTraceGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// ExceptionLabel
			// 
			this.ExceptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ExceptionLabel.AutoEllipsis = true;
			this.ExceptionLabel.Location = new System.Drawing.Point(7, 5);
			this.ExceptionLabel.Name = "ExceptionLabel";
			this.ExceptionLabel.Size = new System.Drawing.Size(580, 50);
			this.ExceptionLabel.TabIndex = 0;
			this.ExceptionLabel.Text = "Exception occurred in {0}, the exception thrown was {1}. Details below.";
			this.ExceptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MessageGroup
			// 
			this.MessageGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessageGroup.Controls.Add(this.ExceptionMessage);
			this.MessageGroup.Location = new System.Drawing.Point(4, 63);
			this.MessageGroup.Name = "MessageGroup";
			this.MessageGroup.Size = new System.Drawing.Size(585, 100);
			this.MessageGroup.TabIndex = 1;
			this.MessageGroup.TabStop = false;
			this.MessageGroup.Text = "Message";
			// 
			// ExceptionMessage
			// 
			this.ExceptionMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ExceptionMessage.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ExceptionMessage.Location = new System.Drawing.Point(3, 16);
			this.ExceptionMessage.Multiline = true;
			this.ExceptionMessage.Name = "ExceptionMessage";
			this.ExceptionMessage.ReadOnly = true;
			this.ExceptionMessage.Size = new System.Drawing.Size(579, 81);
			this.ExceptionMessage.TabIndex = 0;
			// 
			// StackTraceGroup
			// 
			this.StackTraceGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.StackTraceGroup.Controls.Add(this.ExceptionStackTrace);
			this.StackTraceGroup.Location = new System.Drawing.Point(4, 169);
			this.StackTraceGroup.Name = "StackTraceGroup";
			this.StackTraceGroup.Size = new System.Drawing.Size(585, 200);
			this.StackTraceGroup.TabIndex = 2;
			this.StackTraceGroup.TabStop = false;
			this.StackTraceGroup.Text = "Stack Trace";
			// 
			// ExceptionStackTrace
			// 
			this.ExceptionStackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ExceptionStackTrace.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ExceptionStackTrace.Location = new System.Drawing.Point(3, 16);
			this.ExceptionStackTrace.Multiline = true;
			this.ExceptionStackTrace.Name = "ExceptionStackTrace";
			this.ExceptionStackTrace.ReadOnly = true;
			this.ExceptionStackTrace.Size = new System.Drawing.Size(579, 181);
			this.ExceptionStackTrace.TabIndex = 0;
			// 
			// SendButton
			// 
			this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SendButton.Enabled = false;
			this.SendButton.Location = new System.Drawing.Point(5, 385);
			this.SendButton.Name = "SendButton";
			this.SendButton.Size = new System.Drawing.Size(100, 35);
			this.SendButton.TabIndex = 3;
			this.SendButton.Text = "Send error report";
			this.SendButton.UseVisualStyleBackColor = true;
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseButton.Location = new System.Drawing.Point(490, 385);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(100, 35);
			this.CloseButton.TabIndex = 4;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
			// 
			// ExtraInfoCheckbox
			// 
			this.ExtraInfoCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ExtraInfoCheckbox.AutoSize = true;
			this.ExtraInfoCheckbox.Enabled = false;
			this.ExtraInfoCheckbox.Location = new System.Drawing.Point(110, 395);
			this.ExtraInfoCheckbox.Name = "ExtraInfoCheckbox";
			this.ExtraInfoCheckbox.Size = new System.Drawing.Size(152, 17);
			this.ExtraInfoCheckbox.TabIndex = 5;
			this.ExtraInfoCheckbox.Text = "Send info about my system";
			this.ExtraInfoCheckbox.UseVisualStyleBackColor = true;
			// 
			// ExceptionDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(594, 422);
			this.ControlBox = false;
			this.Controls.Add(this.ExtraInfoCheckbox);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.SendButton);
			this.Controls.Add(this.StackTraceGroup);
			this.Controls.Add(this.MessageGroup);
			this.Controls.Add(this.ExceptionLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExceptionDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sharpcraft Exception";
			this.TopMost = true;
			this.MessageGroup.ResumeLayout(false);
			this.MessageGroup.PerformLayout();
			this.StackTraceGroup.ResumeLayout(false);
			this.StackTraceGroup.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label ExceptionLabel;
		private System.Windows.Forms.GroupBox MessageGroup;
		private System.Windows.Forms.TextBox ExceptionMessage;
		private System.Windows.Forms.GroupBox StackTraceGroup;
		private System.Windows.Forms.TextBox ExceptionStackTrace;
		private System.Windows.Forms.Button SendButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.CheckBox ExtraInfoCheckbox;
	}
}