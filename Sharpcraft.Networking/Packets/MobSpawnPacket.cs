/*
 * MobSpawnPacket.cs
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
using System.Collections;
using System.Collections.Generic;
using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class MobSpawnPacket : Packet
	{
		public int EntityID;
		public sbyte MobType;
		public int X;
		public int Y;
		public int Z;
		public sbyte Yaw;
		public sbyte Pitch;
		public sbyte HeadYaw;
		public Dictionary<int, object> Metadata;

		public MobSpawnPacket(Int32 entityId = 0, sbyte mobType = 0, Int32 x = 0, Int32 y = 0, Int32 z = 0,
			sbyte yaw = 0, sbyte pitch = 0, sbyte headYaw = 0, object metadata = null) : base(PacketType.MobSpawn)
		{
			EntityID = entityId;
			MobType = mobType;
			X = x;
			Y = y;
			Z = z;
			Yaw = yaw;
			Pitch = pitch;
			HeadYaw = headYaw;
			Metadata = metadata;
		}
	}
}
