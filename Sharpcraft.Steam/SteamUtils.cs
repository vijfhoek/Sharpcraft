using System;
using System.Text;

namespace Sharpcraft.Steam
{
	internal static class SteamUtils
	{
		internal static byte[] StringToByte(string s)
		{
			return new UTF8Encoding().GetBytes(s);
		}
	}
}
