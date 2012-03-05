/*
 * NamedEntitySpawnPacket.cs
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

using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class NamedEntitySpawnPacket : Packet
	{
		public Int32 EntityID;
		public string PlayerName;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public sbyte Rotation;
		public sbyte Pitch;
		public Int16 CurrentItem;

		public NamedEntitySpawnPacket(Int32 entityId = 0, string playerName = null, Int32 x = 0, Int32 y = 0, Int32 z = 0,
			sbyte rotation = 0, sbyte pitch = 0, Int16 currentItem = 0) : base(PacketType.NamedEntitySpawn)
		{
			EntityID = entityId;
			PlayerName = playerName;
			X = x;
			Y = y;
			Z = z;
			Rotation = rotation;
			Pitch = pitch;
			CurrentItem = currentItem;
		}
	}
}
