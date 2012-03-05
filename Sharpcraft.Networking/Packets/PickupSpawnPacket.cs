/*
 * PickupSpawnPacket.cs
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

using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PickupSpawnPacket : Packet
	{
		public int EntityID;
		public short Item;
		public sbyte Count;
		public short DamageData;
		public int X;
		public int Y;
		public int Z;
		public sbyte Rotation;
		public sbyte Pitch;
		public sbyte Roll;

		public PickupSpawnPacket(Int32 entityId = 0, Int16 item = 0, sbyte count = 0, Int16 damageData = 0, Int32 x = 0, Int32 y = 0, Int32 z = 0,
			sbyte rotation = 0, sbyte pitch = 0, sbyte roll = 0) : base(PacketType.PickupSpawn)
		{
			EntityID = entityId;
			Item = item;
			Count = count;
			DamageData = damageData;
			X = x;
			Y = y;
			Z = z;
			Rotation = rotation;
			Pitch = pitch;
			Roll = roll;
		}
	}
}
