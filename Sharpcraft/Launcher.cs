using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using log4net;

using Sharpcraft.Logging;

namespace Sharpcraft
{
	/// <summary>
	/// Launcher form for Sharpcraft, handles user authentication and game start.
	/// </summary>
	public partial class Launcher : Form
	{
		private delegate void VoidDelegate();

		private readonly ILog _log;

		private Sharpcraft _game;
		private bool _gameRunning;
		private Thread _gameThread;

		private bool _userBoxInactive = true;
		private bool _passBoxInactive = true;

		/// <summary>
		/// Initializes a new instance of the Sharpcraft Launcher.
		/// </summary>
		public Launcher()
		{
			InitializeComponent();
			_log = LoggerManager.GetLogger(this);
			PassBox.PasswordChar = (char) 0x25CF;
			UpdateForm();
			_log.Info("Launcher initialized.");
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
			_log.Debug("Login button clicked.");
			if (!ValidLogin() || _gameRunning)
				return;
			_log.Debug("Disabling launcher");
			Enabled = false;
			_log.Debug("Hiding launcher.");
			Hide();
			try
			{
				// Authenticate user here
				_log.Debug("Creating game thread.");
				_gameThread = new Thread(RunGame);
				//_gameThread.Name = "SharpcraftClientThread";
				_log.Info("Starting game thread...");
				_gameThread.Start();
			}
			catch (System.Net.Sockets.SocketException ex)
			{
				// This should be handled in the game itself when it's finished.
				_log.Error("Failed to connect to target server, " + ex.GetType() + " was thrown.");
				_log.Error(ex.GetType() + ": " + ex.Message);
				_log.Error("Stack Trace:\n" + ex.StackTrace);
				CloseGame();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error occurred while trying to launch the game, more info can be found in the application log.",
								"Sharpcraft Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_log.Error("Failed to launch Sharpcraft, " + ex.GetType() + " thrown.");
				_log.Error("ToString() => " + ex);
				_log.Error(ex.GetType() + ": " + ex.Message);
				_log.Error("Stack Trace:\n" + ex.StackTrace);
				CloseGame();
#if DEBUG
				throw;
#else
				Close();
#endif
			}
		}

		private void RegisterLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.minecraft.net/register");
		}

		private void RunGame()
		{
			_log.Debug("RunGame();");
			_game = new Sharpcraft();
			_log.Debug("Registering to Game.Exiting event.");
			_game.Exiting += GameExit;
			_gameRunning = true;
			_log.Info("Running game...");
			_game.Run();
			_log.Debug("RunGame(); ## END ##");
		}

		private void GameExit(object sender, EventArgs e)
		{
			_log.Info("Game exit detected, shutting down all components...");
			CloseGame();
			_log.Debug("Closing launcher...");
			if (InvokeRequired)
			{
				Invoke((VoidDelegate) (Close));
			}
			else
			{
				Close();
			}
		}

		private void CloseGame()
		{
			_log.Info("Unloading game components...");
			_game.Dispose();
			if (_game.IsActive)
				_game.Exit();
			//_gameThread.Abort(); // Causes application to freeze indefinately.
			//_gameThread.Join();
			if (_game != null)
				_game.Exiting -= GameExit;
			_game = null;
			_gameRunning = false;
			_log.Info("Game components unloaded!");
		}

		private void LauncherFormClosed(object sender, FormClosedEventArgs e)
		{
			Program.Quit();
		}
	}
}
