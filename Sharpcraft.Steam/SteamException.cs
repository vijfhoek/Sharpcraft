using System;
using System.Runtime.Serialization;

namespace Sharpcraft.Steam
{
	public class SteamException : Exception
	{
		public SteamExceptionType Type { get; private set; }

		public SteamException(string message, SteamExceptionType type) : base(message)
		{
			Type = type;
		}
	}
}
