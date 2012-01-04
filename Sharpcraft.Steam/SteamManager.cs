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

namespace Sharpcraft.Steam
{
	public static class SteamManager
	{
		private static Timer _steamWatcher;

		internal static ISteamClient010 Client { get; private set; }
		internal static ISteamFriends002 Friends { get; private set; }
		internal static int Pipe { get; private set; }
		internal static int User { get; private set; }

		public static SteamFriendList FriendList { get; private set; }

		public static event SteamCloseEventHandler OnSteamClose;
		private static void SteamClose()
		{
			if (OnSteamClose != null)
				OnSteamClose();
		}

		public static bool Init()
		{
			try
			{
				if (!Steamworks.Load())
					throw new SteamException("Steamworks failed to load.", SteamExceptionType.SteamworksLoadFail);

				Client = Steamworks.CreateInterface<ISteamClient010>("SteamClient010");
				if (Client == null)
					throw new SteamException("Steamclient failed to load! Is the client running? (Sharpcraft.Steam.SteamManager.Client == null!)",
											 SteamExceptionType.SteamLoadFail);

				Pipe = Client.CreateSteamPipe();
				User = Client.ConnectToGlobalUser(Pipe);

				Friends = Steamworks.CastInterface<ISteamFriends002>(Client.GetISteamFriends(User, Pipe, "SteamFriends002"));

				FriendList = new SteamFriendList();

				_steamWatcher = new Timer(SteamCheck, null, 0, 1000);
			}
			catch (SteamException)
			{
				return false;
			}
			return true;
		}

		private static void SteamCheck(object state)
		{
			bool found = Process.GetProcesses().Any(process => process.ProcessName.ToLower() == "steam");
			if (!found)
			{
				Console.WriteLine("Steam process NOT RUNNING. Closing Steam components...");
				SteamClose();
				FriendList = null;
				_steamWatcher.Dispose();
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
