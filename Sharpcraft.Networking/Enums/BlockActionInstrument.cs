/*
 * BlockActionInstrument.cs
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
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// Different instrument type for note block sent by <see cref="Sharpcraft.Networking.Packets.BlockActionPacket" /> (0x36).
	/// </summary>
	public enum BlockActionInstrument
	{
		/// <summary>
		/// The harp.
		/// </summary>
		Harp			= 0,
		/// <summary>
		/// The double bass.
		/// </summary>
		DoubleBass		= 1,
		/// <summary>
		/// The snare drum.
		/// </summary>
		SnareDrum		= 2,
		/// <summary>
		/// The clicks/sticks.
		/// </summary>
		ClicksSticks	= 3,
		/// <summary>
		/// The bass drum.
		/// </summary>
		BassDrum		= 4
	}
}
