/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System.Diagnostics;
using System.Linq;
using System.Threading;

using Steam4NET;

using log4net;

using Sharpcraft.Logging;

namespace Sharpcraft.Steam
{
	/// <summary>
	/// Static class managing all other Steam components.
	/// </summary>
	public static class SteamManager
	{
		/// <summary>
		/// Log object for this class.
		/// </summary>
		private static ILog _log;

		/// <summary>
		/// Timer for checking Steam process status.
		/// </summary>
		private static Timer _steamWatcher;

		/// <summary>
		/// <see cref="bool" /> indicating whether or not the Steam client is currently running.
		/// </summary>
		public static bool SteamLoaded { get; private set; }

		/// <summary>
		/// The interface for the Steam client.
		/// </summary>
		private static ISteamClient010 Client { get; set; }
		/// <summary>
		/// The interface for Steam friends.
		/// </summary>
		internal static ISteamFriends002 Friends { get; private set; }
		/// <summary>
		/// Steam client pipe.
		/// </summary>
		private static int Pipe { get; set; }
		/// <summary>
		/// Steam user.
		/// </summary>
		private static int User { get; set; }

		/// <summary>
		/// Manages Steam friends.
		/// </summary>
		public static SteamFriendList FriendList { get; private set; }

		/// <summary>
		/// Event fired when the Steam client has closed.
		/// </summary>
		public static event SteamCloseEventHandler OnSteamClose;
		/// <summary>
		/// Method called when the Steam client has closed, calls <see cref="OnSteamClose" /> to notify listeners.
		/// </summary>
		private static void SteamClose()
		{
			_log.Debug("SteamClose event sending!");
			if (OnSteamClose != null)
				OnSteamClose();
		}

		/// <summary>
		/// Initializes the <c>SteamManager</c>.
		/// This MUST be called before other Steam operations are executed.
		/// </summary>
		/// <returns><c>true</c> if everything initialized properly, <c>false</c> otherwise.</returns>
		public static bool Init()
		{
			_log = LoggerManager.GetLogger(typeof(SteamManager));
			try
			{
				if (!Steamworks.Load())
				{
					_log.Error("Steamworks failed to load, throwing exception!");
					throw new SteamException("Steamworks failed to load.", SteamExceptionType.SteamworksLoadFail);
				}

				Client = Steamworks.CreateInterface<ISteamClient010>("SteamClient010");
				if (Client == null)
				{
					_log.Error("Steamclient is NULL!! Throwing exception!");
					throw new SteamException("Steamclient failed to load! Is the client running? (Sharpcraft.Steam.SteamManager.Client == null!)",
											 SteamExceptionType.SteamLoadFail);
				}

				Pipe = Client.CreateSteamPipe();
				User = Client.ConnectToGlobalUser(Pipe);

				Friends = Steamworks.CastInterface<ISteamFriends002>(Client.GetISteamFriends(User, Pipe, "SteamFriends002"));

				if (Friends == null)
					return false;

				FriendList = new SteamFriendList();

				_steamWatcher = new Timer(SteamCheck, null, 0, 1000);
			}
			catch (SteamException ex)
			{
				_log.Warn("Warning! SteamManager caught a SteamException exception, returning FALSE. Steam components will NOT LOAD!");
				_log.Warn("The SteamException type was: " + System.Enum.GetName(typeof(SteamExceptionType), ex.Type));
				return false;
			}
			_log.Info("SteamManager has been initialized!");
			SteamLoaded = true;
			return true;
		}

		/// <summary>
		/// Unload all Steam components and handlers.
		/// </summary>
		public static void Close()
		{
			_log.Debug("Close();");
			if (SteamLoaded)
			{
				_log.Info("Unloading Steam components...");
				CallbackDispatcher.SpawnDispatchThread(Pipe);
				_log.Info("Waiting for dispatch thread to finish...");
				//Thread.Sleep(5000); // Do we need this?
				CallbackDispatcher.StopDispatchThread(Pipe);
				_log.Info("Steam dispatch thread stopped.");
				_log.Info("Releasing Steam user and Steam client...");
				Client.ReleaseUser(Pipe, User);
				Client.ReleaseSteamPipe(Pipe);
				_log.Info("Steam components unloaded, setting SteamLoaded to FALSE.");
				SteamLoaded = false;
			}
			_log.Debug("Close(); ## END ##");
		}

		/// <summary>
		/// Check if the Steam client is running, shut down all Steam components if it's not.
		/// </summary>
		/// <param name="state">N/A (Not Used)</param>
		private static void SteamCheck(object state)
		{
			//_log.Info("Running Steam process check..."); // Gets VERY spammy in the log.
			bool found = Process.GetProcesses().Any(process => process.ProcessName.ToLower() == "steam");
			if (!found || Friends == null)
			{
				_log.Info("Steam process not running, closing Steam components...");
				_log.Debug("Sending SteamClose event!");
				SteamClose();
				FriendList = null;
				_steamWatcher.Dispose();
				SteamLoaded = false;
			}
		}

		/// <summary>
		/// Get the name of the currently logged in user.
		/// </summary>
		/// <returns></returns>
		public static string GetName()
		{
			return Friends.GetPersonaName();
		}

		/// <summary>
		/// Get the <see cref="EPersonaState" /> of the currently logged in user.
		/// </summary>
		/// <returns></returns>
		public static EPersonaState GetState()
		{
			return Friends.GetPersonaState();
		}

		/// <summary>
		/// Get a string representation of the currently logged in user's <see cref="EPersonaState" />.
		/// </summary>
		/// <param name="pretty">If <c>true</c>, capitalize the first letter of the return value.</param>
		/// <returns>The user's <see cref="EPersonaState" /> in a string format.</returns>
		public static string GetStatus(bool pretty = false)
		{
			return SteamUtils.StateToStatus(GetState(), pretty);
		}
	}
}
