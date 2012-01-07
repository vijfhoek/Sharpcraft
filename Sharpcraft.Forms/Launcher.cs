using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sharpcraft.Forms
{
	public partial class Launcher : Form
	{
		private bool _userBoxInactive = true;
		private bool _passBoxInactive = true;

		public Launcher()
		{
			InitializeComponent();
			InterfacePanel.BackColor = Color.FromArgb(155, Color.White);
			PassBox.PasswordChar = (char) 0x25CF;
			UpdateForm();
		}

		private bool ValidLogin()
		{
			bool validUser = !_userBoxInactive && !string.IsNullOrEmpty(UserBox.Text);
			bool validPass = !_passBoxInactive && !string.IsNullOrEmpty(PassBox.Text);
			return validUser && validPass;
		}

		private void UpdateForm()
		{
			LoginButton.Enabled = ValidLogin();
		}

		private void UserBoxEnter(object sender, EventArgs e)
		{
			if (_userBoxInactive)
			{
				UserBox.Text = string.Empty;
				UserBox.ForeColor = Color.Black;
				_userBoxInactive = false;
			}
			UpdateForm();
		}

		private void UserBoxTextChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void UserBoxLeave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(UserBox.Text))
			{
				UserBox.Text = "Username";
				UserBox.ForeColor = Color.DimGray;
				_userBoxInactive = true;
			}
			UpdateForm();
		}

		private void PassBoxEnter(object sender, EventArgs e)
		{
			if (_passBoxInactive)
			{
				PassBox.Text = string.Empty;
				PassBox.ForeColor = Color.Black;
				_passBoxInactive = false;
			}
			UpdateForm();
		}

		private void PassBoxTextChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void PassBoxLeave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(PassBox.Text))
			{
				PassBox.Text = "Password";
				PassBox.ForeColor = Color.DimGray;
				_passBoxInactive = true;
			}
			UpdateForm();
		}

		private void LoginButtonClick(object sender, EventArgs e)
		{
			if (!ValidLogin())
				return;
			MessageBox.Show("TODO: Launch the game here.");
		}

		private void RegisterLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.minecraft.net/register");
		}
	}
}
