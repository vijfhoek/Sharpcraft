using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Steam4NET;

namespace Sharpcraft.Steam
{
	public static class SteamManager
	{
		internal static ISteamClient010 Client;
		internal static ISteamFriends002 Friends;
		internal static int Pipe;
		internal static int User;

		public static bool Init()
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

			return true;
		}
	}
}
