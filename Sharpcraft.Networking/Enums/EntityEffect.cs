/*
 * EntityEffect.cs
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
	/// The different entity effects.
	/// </summary>
	/// <remarks>http://wiki.vg/Protocol#Effects</remarks>
	public enum EntityEffect
	{
		/// <summary>
		/// Increases players speed and FOV.
		/// </summary>
		MoveSpeed		=  1,

		/// <summary>
		/// Decreases player speed and FOV.
		/// </summary>
		MoveSlowDown	=  2,

		/// <summary>
		/// Increases player dig speed.
		/// </summary>
		DigSpeed		=  3,

		/// <summary>
		/// Decreases player dig speed.
		/// </summary>
		/// <remarks>
		/// Caused by golden apple.
		/// Health regenerates over 600-tick (30s) period.
		/// </remarks>
		Regeneration	= 10,
		Resistance		= 11,
		FireResistance	= 12,

		/// <summary>
		/// Bubbles do not decrease underwater.
		/// </summary>
		WaterBreathing	= 13,
		Invisibility	= 14,
		Blindness		= 15,
		NightVision		= 16,

		/// <summary>
		/// Food bar turns green.
		/// </summary>
		/// <remarks>
		/// Caused by poisoning from Rotten Flesh or Raw Chicken.
		/// </remarks>
		Hunger			= 17,
		Weakness		= 18,

		/// <summary>
		/// Hearts turn yellow.
		/// </summary>
		/// <remarks>
		/// Caused by poisoning from cave (blue) spider.
		/// </remarks>
		Poison			= 19
	}
}
