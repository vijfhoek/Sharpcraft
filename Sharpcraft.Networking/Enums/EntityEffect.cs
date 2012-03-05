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
		MoveSpeed		=  1,
		MoveSlowDown	=  2,
		DigSpeed		=  3,
		DigSlowDown		=  4,
		DamageBoost		=  5,
		Heal			=  6,
		Harm			=  7,
		Jump			=  8,
		Confusion		=  9,
		Regeneration	= 10,
		Resistance		= 11,
		FireResistance	= 12,
		WaterBreathing	= 13,
		Invisibility	= 14,
		Blindness		= 15,
		NightVision		= 16,
		Hunger			= 17,
		Weakness		= 18,
		Poison			= 19
	}
}
