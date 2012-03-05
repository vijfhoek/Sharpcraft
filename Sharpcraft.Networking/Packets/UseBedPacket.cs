/*
 * UseBedPacket.cs
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
	public class UseBedPacket : Packet
	{
		public Int32 EntityID;
		public sbyte InBed; // ????: http://wiki.vg/Protocol#Use_Bed_.280x11.29
		public Int32 X;
		public sbyte Y;
		public Int32 Z;

		public UseBedPacket(Int32 entityId = 0, sbyte inBed = 0, Int32 x = 0, sbyte y = 0, Int32 z = 0) : base(PacketType.UseBed)
		{
			EntityID = entityId;
			InBed = inBed;
			X = x;
			Y = y;
			Z = z;
		}
	}
}
