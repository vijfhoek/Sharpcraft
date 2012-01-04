/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Steam4NET;

using log4net;

using Sharpcraft.Logging;

namespace Sharpcraft.Steam
{
	public static class SteamManager
	{
		private static ILog _log;

		private static Timer _steamWatcher;

		public static bool SteamLoaded { get; private set; }

		internal static ISteamClient010 Client { get; private set; }
		internal static ISteamFriends002 Friends { get; private set; }
		internal static int Pipe { get; private set; }
		internal static int User { get; private set; }

		public static SteamFriendList FriendList { get; private set; }

		public static event SteamCloseEventHandler OnSteamClose;
		private static void SteamClose()
		{
			_log.Debug("SteamClose event sending!");
			if (OnSteamClose != null)
				OnSteamClose();
		}

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
			catch (SteamException)
			{
				_log.Warn("Warning! SteamManager caught a SteamException exception, returning FALSE. Steam components will NOT LOAD!");
				return false;
			}
			_log.Info("SteamManager has been initialized!");
			SteamLoaded = true;
			return true;
		}

		public static void Close()
		{
			_log.Debug("Close();");
			if (SteamLoaded)
			{
				_log.Info("Unloading Steam components...");
				CallbackDispatcher.SpawnDispatchThread(Pipe);
				_log.Info("Waiting for dispatch thread to finish...");
				Thread.Sleep(5000); // Do we need this?
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

		public static string GetName()
		{
			return Friends.GetPersonaName();
		}

		public static EPersonaState GetState()
		{
			return Friends.GetPersonaState();
		}

		public static string GetStatus(bool pretty = false)
		{
			return SteamUtils.StateToStatus(GetState(), pretty);
		}
	}
}
