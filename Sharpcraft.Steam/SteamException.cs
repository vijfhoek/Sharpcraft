/* 
 * Sharpcraft.Steam
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;

namespace Sharpcraft.Steam
{
	[Serializable]
	public class SteamException : Exception
	{
		public SteamExceptionType Type { get; private set; }

		public SteamException(string message, SteamExceptionType type) : base(message)
		{
			Type = type;
		}
	}
}
