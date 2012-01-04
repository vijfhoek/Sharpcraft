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

		public string GetStatus(bool pretty = false)
		{
			return SteamUtils.StateToStatus(GetState(), pretty);
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
