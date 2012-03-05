/*
 * EntityStatus.cs
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
	/// Differet status codes sent by <see cref="Sharpcraft.Networking.Packets.EntityStatusPacket" /> (0x26).
	/// </summary>
	public enum EntityStatus
	{
		/// <summary>
		/// Entity hurt.
		/// </summary>
		EntityHurt	= 2,
		/// <summary>
		/// Entity dead/died.
		/// </summary>
		EntityDead	= 3,
		/// <summary>
		/// Wolf is being tamed.
		/// </summary>
		WolfTaming	= 6,
		/// <summary>
		/// Wolf has been tamed.
		/// </summary>
		WolfTamed	= 7,
		/// <summary>
		/// Wolf is shaking water off itself.
		/// </summary>
		WolfShake	= 8,
		/// <summary>
		/// Eating accepted by server.
		/// </summary>
		EatAccept	= 9
	}
}
