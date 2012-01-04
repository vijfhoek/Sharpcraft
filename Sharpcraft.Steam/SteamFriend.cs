using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Steam4NET;

namespace Sharpcraft.Steam
{
	public class SteamFriend
	{
		public readonly CSteamID SteamID;
		
		internal SteamFriend(CSteamID steamID)
		{
			SteamID = steamID;
		}

		public string GetName()
		{
			return SteamManager.Friends.GetFriendPersonaName(SteamID);
		}

		public EPersonaState GetState()
		{
			return SteamManager.Friends.GetFriendPersonaState(SteamID);
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

		public void SendMessage(string message)
		{
			byte[] msg = SteamUtils.StringToByte(message);
			SteamManager.Friends.SendMsgToFriend(SteamID, EChatEntryType.k_EChatEntryTypeChatMsg, msg, msg.Length);
		}

		public void SendEmote(string emote)
		{
			byte[] em = SteamUtils.StringToByte(emote);
			SteamManager.Friends.SendMsgToFriend(SteamID, EChatEntryType.k_EChatEntryTypeEmote, em, em.Length);
		}
	}
}
