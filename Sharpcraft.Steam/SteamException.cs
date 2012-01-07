/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;

namespace Sharpcraft.Steam
{
	/// <summary>
	/// Exception thrown when a Steam component encounters an error.
	/// </summary>
	[Serializable]
	public class SteamException : Exception
	{
		/// <summary>
		/// <see cref="SteamExceptionType" /> representing the type of exception thrown.
		/// </summary>
		public SteamExceptionType Type { get; private set; }

		/// <summary>
		/// Initialize a new instance of <c>SteamException</c>.
		/// </summary>
		/// <param name="message">Message associated with the <c>SteamException</c>.</param>
		/// <param name="type"><see cref="SteamExceptionType" /> representing the type of exception thrown.</param>
		public SteamException(string message, SteamExceptionType type) : base(message)
		{
			Type = type;
		}
	}
}
