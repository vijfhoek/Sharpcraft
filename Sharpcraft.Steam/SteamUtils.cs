/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System.Text;

using Steam4NET;

namespace Sharpcraft.Steam
{
	internal static class SteamUtils
	{
		internal static byte[] StringToByte(string s)
		{
			return new UTF8Encoding().GetBytes(s);
		}

		internal static string StateToStatus(EPersonaState state, bool pretty = false)
		{
			string status;
			switch (state)
			{
				case EPersonaState.k_EPersonaStateAway:
					status = "away";
					break;
				case EPersonaState.k_EPersonaStateBusy:
					status = "busy";
					break;
				case EPersonaState.k_EPersonaStateMax: // What is this, in-game?
					status = "max";
					break;
				case EPersonaState.k_EPersonaStateOffline:
					status = "offline";
					break;
				case EPersonaState.k_EPersonaStateOnline:
					status = "online";
					break;
				case EPersonaState.k_EPersonaStateSnooze:
					status = "snooze";
					break;
				default:
					status = "undefined";
					break;
			}
			if (pretty)
			{
				char[] a = status.ToCharArray();
				a[0] = char.ToUpper(a[0]);
				return new string(a);
			}
			return status;
		}
	}
}
