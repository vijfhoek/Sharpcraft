using System;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

using Newtonsoft.Json;

using Sharpcraft.Logging;
using Sharpcraft.Networking;
using Sharpcraft.Library.Configuration;

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
		/// The settings specific for the launcher.
		/// </summary>
		private readonly LauncherSettings _settings;

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
		/// The version of Sharpcraft, as set in AssemblyInfo.cs.
		/// </summary>
		private readonly string _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		/// <summary>
		/// The full SHA hash of this git commit.
		/// </summary>
		private readonly string _hash = "DEVELOPMENT";

		/// <summary>
		/// Shorter, more readable version of the git commit hash.
		/// </summary>
		private readonly string _shortHash = "DEVELOPMENT";

		/// <summary>
		/// The dev who authored the commit.
		/// </summary>
		private readonly string _author = "DEVELOPMENT";

		/// <summary>
		/// Time when the commit was made.
		/// </summary>
		private readonly DateTime _time = DateTime.Now;

		/// <summary>
		/// Format for the version string used by the <see cref="VersionString" />.
		/// </summary>
		/// <remarks><c>{0}</c> = version, <c>{1}</c> = short hash, <c>{2}</c> = author, <c>{3}</c> = time.</remarks>
		private const string VersionFormat = "Version {0} ({1}) by {2} at {3}";

		/// <summary>
		/// Format for the GitHub link used by the <see cref="LinkString" />.
		/// </summary>
		/// <remarks><c>{0}</c> = author, <c>{1}</c> = full commit hash.</remarks>
		private const string LinkFormat = "https://github.com/{0}/Sharpcraft/commit/{1}";

		/// <summary>
		/// The version string using the <see cref="VersionFormat" />.
		/// </summary>
		private string VersionString { get { return string.Format(VersionFormat, _version, _shortHash, _author, _time); } }
		
		/// <summary>
		/// The GitHub link using the <see cref="LinkFormat" />.
		/// </summary>
		private string LinkString { get { return string.Format(LinkFormat, _author, _hash); } }

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
			if (File.Exists(SharpcraftConstants.GitInfoFile))
			{
				using (var reader = new StreamReader(SharpcraftConstants.GitInfoFile))
				{
					string content = reader.ReadLine();
					if (!string.IsNullOrEmpty(content))
					{
						string[] gitInfo = content.Split(':');
						if (gitInfo.Length >= 4)
						{
							_hash = gitInfo[0];
							_shortHash = gitInfo[1];
							_author = gitInfo[2];
							if (_author.Contains(" "))
								_author = _author.Split(' ')[0];
							_time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(double.Parse(gitInfo[3])).ToLocalTime();
						}
					}
				}
			}

			VersionLabel.Text = VersionString;

			if (File.Exists(SharpcraftConstants.LauncherSettings))
			{
				_log.Info("Loading launcher settings from file...");
				var reader = new StreamReader(SharpcraftConstants.LauncherSettings);
				_settings = new JsonSerializer().Deserialize<LauncherSettings>(new JsonTextReader(reader));
				_log.Info("Launcher settings loaded successfully!");
				reader.Close();
			}
			else
			{
				_settings = new LauncherSettings(SharpcraftConstants.LauncherSettings);
			}
			if (!string.IsNullOrEmpty(_settings.Username))
			{
				_userBoxInactive = false;
				UserBox.Text = _settings.Username;
				UserBox.ForeColor = Color.Black;
			}
			RememberCheckbox.Checked = _settings.Remember;
			if (_settings.Remember && !string.IsNullOrEmpty(_settings.GetPassword()))
			{
				_passBoxInactive = false;
				PassBox.Text = _settings.GetPassword();
				PassBox.ForeColor = Color.Black;
			}
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
		/// Opens the GitHub URL in browser to show info about the commit.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See MSDN)</param>
		/// <param name="e">N/A (Not Used) (See MSDN)</param>
		private void VersionLabelClick(object sender, EventArgs e)
		{
			Process.Start(LinkString);
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
			_settings.Username = _user.GetName();
			_settings.Remember = RememberCheckbox.Checked;
			if (_settings.Remember)
				_settings.SetPassword(PassBox.Text);
			_settings.WriteToFile();
			_loginThread = new Thread(() => _auth.Login(_user.GetName(), PassBox.Text)) {Name = "Login"};
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
				_gameThread = new Thread(RunGame) {Name = "Game"};
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
