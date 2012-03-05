/*
 * Animation.cs
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

namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// The different animation types sent by the Animation packet (0x12).
	/// </summary>
	public enum Animation
	{
		/// <summary>
		/// No animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		NoAnimation		= 0,
		/// <summary>
		/// Swing arm (e.g. when attacking).
		/// </summary>
		SwingArm		= 1,
		/// <summary>
		/// Damage animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		DamageAnimation	= 2,
		/// <summary>
		/// Leave bed animation.
		/// </summary>
		LeaveBed		= 3,
		/// <summary>
		/// Eat food animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		EatFood			= 5,
		/// <summary>
		/// Unknown.
		/// </summary>
		Unknown			= 102,
		/// <summary>
		/// Crouch animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		Crouch			= 104,
		/// <summary>
		/// Uncrouch animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		Uncrouch		= 105
	}
}
