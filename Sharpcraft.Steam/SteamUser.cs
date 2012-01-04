using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Steam4NET;

namespace Sharpcraft.Steam
{
	public class SteamUser
	{
		private readonly ISteamFriends002 _friends;

		public readonly CSteamID SteamID;
		
		internal SteamUser(CSteamID steamID)
		{
			SteamID = steamID;
		}

		public string GetName()
		{
			return _friends.GetFriendPersonaName(SteamID);
		}

		public EPersonaState GetState()
		{
			return _friends.GetFriendPersonaState(SteamID);
		}

		public string GetStatus()
		{
			string state;
			switch(GetState())
			{
				case EPersonaState.k_EPersonaStateAway:
					state = "away";
					break;
				case EPersonaState.k_EPersonaStateBusy:
					state = "busy";
					break;
				case EPersonaState.k_EPersonaStateMax:
					state = "max";
					break;
				case EPersonaState.k_EPersonaStateOffline:
					state = "offline";
					break;
				case EPersonaState.k_EPersonaStateOnline:
					state = "online";
					break;
				case EPersonaState.k_EPersonaStateSnooze:
					state = "snooze";
					break;
				default:
					state = "undefined";
					break;
			}
			return state;
		}

		public bool IsOnline()
		{
			return GetState() == EPersonaState.k_EPersonaStateOnline;
		}
	}
}
