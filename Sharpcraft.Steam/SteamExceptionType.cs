/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

namespace Sharpcraft.Steam
{
	/// <summary>
	/// <see cref="System.Enum" /> containing all possible Steam exceptions.
	/// </summary>
	public enum SteamExceptionType
	{
		/// <summary>
		/// Steamworks failed to load.
		/// </summary>
		SteamworksLoadFail,
		/// <summary>
		/// Failed to integrate with Steam client.
		/// </summary>
		SteamLoadFail,
		/// <summary>
		/// Could not create Steam pipe.
		/// </summary>
		SteamPipeFail,
		/// <summary>
		/// Could not create user pipe.
		/// </summary>
		SteamGlobalUserFail
	}
}
