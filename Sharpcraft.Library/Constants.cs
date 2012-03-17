/*
 * Constants.cs
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

namespace Sharpcraft.Library
{
	/// <summary>
	/// Contants related <see cref="Sharpcraft.Library" />.
	/// </summary>
	public static class Constants
	{
		/// <summary>
		/// Directory where data is stored.
		/// </summary>
		public const string DataDirectory = "data";

		/// <summary>
		/// Directory where Minecraft data is stored.
		/// </summary>
		public static string MinecraftDataDirectory { get { return DataDirectory + "\\minecraft"; } }

		/// <summary>
		/// File containing item definitions.
		/// </summary>
		public static string MinecraftItemFile { get { return MinecraftDataDirectory + "\\items.list"; } }

		/// <summary>
		/// Regex pattern to filter out username and message from a chat message.
		/// </summary>
		public const string ChatMessageFilterRegex = @"^<(\w+)>\s+(.+)$";

		/// <summary>
		/// Regex pattern to validate chat messages.
		/// </summary>
		public const string ValidChatMessageRegex = @"[^ a-zA-Z0-9\\\[\]\(\)!""#\$%&'\*\+,\-\.\/:;<=>\?@\^_\{\}\|~¦ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜø£Ø×ƒáíóúñÑªº¿®¬½¼¡«»]";
	}
}
