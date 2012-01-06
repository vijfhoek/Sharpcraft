/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System.Text;

using Steam4NET;

namespace Sharpcraft.Steam
{
	/// <summary>
	/// Static class providing various utilities for Steam components.
	/// </summary>
	internal static class SteamUtils
	{
		/// <summary>
		/// Convert a string to a byte array.
		/// </summary>
		/// <param name="source">The string to convert.</param>
		/// <returns>A byte array representing the string passed, UTF8 encoded.</returns>
		internal static byte[] StringToByte(string source)
		{
			return new UTF8Encoding().GetBytes(source);
		}

		/// <summary>
		/// Convert a <see cref="EPersonaState" /> to a string.
		/// </summary>
		/// <param name="state">The state to convert.</param>
		/// <param name="pretty">If <c>true</c>, capitalize the first letter of the string.</param>
		/// <returns>The string representation of the <see cref="EPersonaState" />.</returns>
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
				case EPersonaState.k_EPersonaStateMax: // What is this?
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
