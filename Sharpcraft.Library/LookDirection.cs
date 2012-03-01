/*
 * LookDirection.cs
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

namespace Sharpcraft.Library
{
	/// <summary>
	/// Specifies what direction a player/mob is looking in.
	/// </summary>
	public class LookDirection
	{
		/// <summary>
		/// Yaw.
		/// </summary>
		public float Yaw;

		/// <summary>
		/// Pitch.
		/// </summary>
		public float Pitch;

		/// <summary>
		/// Initialize a new <see cref="LookDirection" /> object.
		/// </summary>
		/// <param name="yaw">Yaw.</param>
		/// <param name="pitch">Pitch.</param>
		public LookDirection(float yaw = 0.0f, float pitch = 0.0f)
		{
			Yaw = yaw;
			Pitch = pitch;
		}
	}
}
