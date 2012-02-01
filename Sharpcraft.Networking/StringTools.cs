using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Sharpcraft.Networking
{
	/// <summary>
	/// Various string tools/converters.
	/// </summary>
	/// <remarks>This uses BigEndianUnicode formattings.</remarks>
	public static class StringTools
	{
		/// <summary>
		/// Convert a string to a byte array.
		/// </summary>
		/// <param name="str">The string to convert.</param>
		/// <returns>String as a byte array.</returns>
		public static byte[] StringToBytes(string str)
		{
			byte[] strLength = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(str.Length));
			List<Byte> bytes = strLength.ToList();

			byte[] bteString = Encoding.BigEndianUnicode.GetBytes(str);
			bytes.AddRange(bteString);

			return bytes.ToArray();
		}

		/// <summary>
		/// Convert a byte array to string.
		/// </summary>
		/// <param name="bytes">The byte array to convert.</param>
		/// <returns>Byte array as a string.</returns>
		public static string BytesToString(byte[] bytes)
		{
			byte[] bteStrLength = { bytes[0], bytes[1] };
			int strLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bteStrLength, 0));
			var str = string.Empty;

			for (short s = 1; s < strLength + 1; s++)
			{
				byte[] tmp = { bytes[s * 2], bytes[(s * 2) + 1] };
				str += Encoding.BigEndianUnicode.GetString(tmp);
			}

			return str;
		}
	}
}
