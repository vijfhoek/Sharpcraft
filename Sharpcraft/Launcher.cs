using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

using Sharpcraft.Logging;
using Sharpcraft.Networking;

namespace Sharpcraft
{
	/// <summary>
	/// Launcher form for Sharpcraft, handles user authentication and game start.
	/// </summary>
	public partial class Launcher : Form
	{
		/// <summary>
		/// Void delegate for use with invoke.
		/// </summary>
		private delegate void VoidDelegate();

		/// <summary>
		/// Log object for this class.
		/// </summary>
		private readonly log4net.ILog _log;

		/// <summary>
		/// Sharpcraft game object.
		/// </summary>
		private Sharpcraft _game;

		/// <summary>
		/// Whether or not the game is currently running.
		/// </summary>
		private bool _gameRunning;

		/// <summary>
		/// Thread for the game to run in.
		/// </summary>
		private Thread _gameThread;

		/// <summary>
		/// Thread for connecting to minecraft.net and authenticating.
		/// </summary>
		private Thread _loginThread;

		/// <summary>
		/// The authenticator object.
		/// </summary>
		private readonly Authenticator _auth;

		/// <summary>
		/// The user.
		/// </summary>
		private User _user;

		/// <summary>
		/// The current minecraft client version.
		/// </summary>
		private const int McVersion = 50;

		/// <summary>
		/// Whether or not the username box is "inactive".
		/// (Has no text and is out of focus).
		/// </summary>
		private bool _userBoxInactive = true;

		/// <summary>
		/// Whether or not the password box is "inactive".
		/// (Has no text and is out of focus).
		/// </summary>
		private bool _passBoxInactive = true;

		/// <summary>
		/// Initializes a new instance of the Sharpcraft Launcher.
		/// </summary>
		public Launcher()
		{
			InitializeComponent();
			_log = LogManager.GetLogger(this);
			PassBox.PasswordChar = (char) 0x25CF;
			_auth = new Authenticator(McVersion);
			_auth.OnLoginEvent += LoginEvent;
			UpdateForm();
			_log.Info("Launcher initialized.");
		}

		/// <summary>
		/// Checks the username and password boxes to determine if they are valid.
		/// </summary>
		/// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
		private bool ValidLogin()
		{
			bool validUser = !_userBoxInactive && !string.IsNullOrEmpty(UserBox.Text);
			bool validPass = !_passBoxInactive && !string.IsNullOrEmpty(PassBox.Text);
			return validUser && validPass;
		}

		/// <summary>
		/// Updates the form controls.
		/// </summary>
		private void UpdateForm()
		{
			LoginButton.Enabled = ValidLogin();
		}

		/// <summary>
		/// Sets the state of the form.
		/// </summary>
		/// <param name="enabled"><c>true</c> to enable and show the form, <c>false</c> to disable and hide it.</param>
		private void SetState(bool enabled)
		{
			if (InvokeRequired)
				Invoke((VoidDelegate)(() => { Enabled = enabled; }));
			else
				Enabled = enabled;

			if (enabled)
			{
				
				if (InvokeRequired)
				{
					Invoke((VoidDelegate) (Show));
					Invoke((VoidDelegate) (BringToFront));
				}
				else
				{
					Show();
					BringToFront();
				}
			}
			else
			{
				if (InvokeRequired)
					Invoke((VoidDelegate) (Hide));
				else
					Hide();
			}
		}

		/// <summary>
		/// Handler for when the username box gets focus-
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
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

		/// <summary>
		/// Handler for when the text in the username box changes.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void UserBoxTextChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		/// <summary>
		/// Handler for when the username box loses focus.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
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

		/// <summary>
		/// Handler for when the password box gains focus.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
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

		/// <summary>
		/// Handler for when the text in the password box changes.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void PassBoxTextChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		/// <summary>
		/// Handler for when the password box loses focus.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
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

		/// <summary>
		/// Handler for when user presses the login button.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void LoginButtonClick(object sender, EventArgs e)
		{
			_log.Debug("Login button clicked.");
			if (!ValidLogin() || _gameRunning)
				return;
			Enabled = false;
			_user = new User(UserBox.Text);
			_loginThread = new Thread(() => _auth.Login(_user.GetName(), PassBox.Text));
			_loginThread.Start();
		}

		/// <summary>
		/// Login event handler.
		/// </summary>
		/// <param name="e"><see cref="LoginEventArgs" /> for the event.</param>
		private void LoginEvent(LoginEventArgs e)
		{
			LoginResult result = e.Result;
			if (!result.Success)
			{
				MessageBox.Show("Failed to login!\n" + result.Result, "Login Failed",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
				SetState(true);
				return;
			}
			_log.Info("Login succeeded! Got result: " + result.Result);
			_user.SetName(result.RealName);
			_user.SetSessionID(result.SessionID);
			try
			{
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
				SetState(true);
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
				SetState(true);
#if DEBUG
				throw;
#else
				Close();
#endif
			}
		}

		/// <summary>
		/// Handler for when user clicks the "Register" link.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		/// <remarks>Opens the URL "http://www.minecraft.net/register" using <see cref="Process" />.Start();</remarks>
		private void RegisterLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.minecraft.net/register");
		}

		/// <summary>
		/// Runs the game.
		/// </summary>
		private void RunGame()
		{
			_log.Debug("RunGame();");
			_game = new Sharpcraft(_user);
			_log.Debug("Registering to Game.Exiting event.");
			_game.Exiting += GameExit;
			_gameRunning = true;
			_log.Info("Running game...");
			SetState(false);
			_game.Run();
			_log.Debug("RunGame(); ## END ##");
		}

		/// <summary>
		/// Event handler for Game.Exiting event.
		/// </summary>
		/// <param name="sender">N/A (Not Used)</param>
		/// <param name="e">N/A (Not Used)</param>
		private void GameExit(object sender, EventArgs e)
		{
			_log.Info("Game exit detected, shutting down all components...");
			CloseGame();
			_log.Debug("Closing launcher...");
			if (InvokeRequired)
				Invoke((VoidDelegate) (Close));
			else
				Close();
		}

		/// <summary>
		/// Close the game and unload components.
		/// </summary>
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

		/// <summary>
		/// Fires when the launcher closes, sends close request to main program and exits application.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void LauncherFormClosed(object sender, FormClosedEventArgs e)
		{
			Program.Quit();
		}
	}
}
