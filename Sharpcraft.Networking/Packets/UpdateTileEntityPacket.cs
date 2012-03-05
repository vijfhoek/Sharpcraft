/*
 * UpdateTileEntityPacket.cs
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
	public class UpdateTileEntityPacket : Packet
	{
		public int X;
		public short Y;
		public int Z;
		public sbyte Action;
		public int Custom1;
		public int Custom2;
		public int Custom3;

		public UpdateTileEntityPacket(Int32 x = 0, Int16 y = 0, Int32 z = 0, sbyte action = 0,
			Int32 custom1 = 0, Int32 custom2 = 0, Int32 custom3 = 0) : base(PacketType.UpdateTileEntity)
		{
			X = x;
			Y = y;
			Z = z;
			Action = action;
			Custom1 = custom1;
			Custom2 = custom2;
			Custom3 = custom3;
		}
	}
}
