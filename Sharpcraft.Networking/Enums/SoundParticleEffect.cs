/*
 * SoundParticleEffect.cs
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
	/// Different sound/particle effects.
	/// </summary>
	/// <remarks>http://wiki.vg/Protocol#Effects_2</remarks>
	public enum SoundParticleEffect
	{
		Click2			= 1000,
		Click1			= 1001,
		BowFire			= 1002,
		DoorToggle		= 1003,
		Extinguish		= 1004,
		RecordPlay		= 1005,
		Charge			= 1007,
		FireballA		= 1008,
		FireballB		= 1009,
		Smoke			= 2000,
		BlockBreak		= 2001,
		SplashPotion	= 2002,
		Portal			= 2003,
		Blaze			= 2004
	}
}
