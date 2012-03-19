/*
 * ChatMessage.cs
 * 
 * Copyright © 2011-2012 by Sijmen Schoon and Adam Hellberg.
 * 
 * This file is part of Sharpcraft.
 * 
 * Sharpcraft is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Sharpcraft is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Sharpcraft.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Disclaimer: Sharpcraft is in no way affiliated with Mojang AB and/or
 * any of its employees and/or licensors.
 * Sijmen Schoon and Adam Hellberg do not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using System.Text.RegularExpressions;

namespace Sharpcraft.Library.Minecraft
{
	/// <summary>
	/// Filters a raw chat message to retrieve username and message sent.
	/// </summary>
	public class ChatMessage
	{
		/// <summary>
		/// The name of the user who sent the message.
		/// </summary>
		public string User { get; private set; }
		
		/// <summary>
		/// The contents of the message.
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// Initialize a new <see cref="ChatMessage" /> object.
		/// </summary>
		/// <param name="raw"></param>
		public ChatMessage(string raw)
		{
			ParseRawString(raw);
		}

		/// <summary>
		/// Parse a raw message to extract username and message.
		/// </summary>
		/// <param name="raw">The raw message to parse, as sent by the server.</param>
		private void ParseRawString(string raw)
		{
			Match match = new Regex(Constants.ChatMessageFilterRegex, RegexOptions.IgnoreCase).Match(raw);
			if (match.Success)
			{
				User = match.Groups[1].ToString();
				Message = match.Groups[2].ToString();
			}
			else
			{
				User = string.Empty;
				Message = raw;
			}
		}
	}
}
