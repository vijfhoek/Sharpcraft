/*
 * ExplosionPacket.cs
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
	public class ExplosionPacket : Packet
	{
		public double X;
		public double Y;
		public double Z;
		public float Unknown; // Radius?
		public Int32 Count;
		public sbyte[,] Records;

		public ExplosionPacket(double x = 0.0, double y = 0.0, double z = 0.0, float unknown = 0.0f,
			Int32 count = 0, sbyte[,] records = null) : base(PacketType.Explosion)
		{
			X = x;
			Y = y;
			Z = z;
			Unknown = unknown;
			Count = count;
			Records = records;
		}
	}
}
