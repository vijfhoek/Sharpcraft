/*
 * MapChunkPacket.cs
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
	public class MapChunkPacket : Packet
	{
		public Int32 X;
		public Int16 Y;
		public Int32 Z;
		public sbyte SizeX;
		public sbyte SizeY;
		public sbyte SizeZ;
		public Int32 CompressedSize;
		public sbyte[] CompressedData;

		public MapChunkPacket(Int32 x = 0, Int16 y = 0, Int32 z = 0, sbyte sizeX = 0, sbyte sizeY = 0, sbyte sizeZ = 0,
			Int32 compressedSize = 0, sbyte[] compressedData = null) : base(PacketType.MapChunk)
		{
			X = x;
			Y = y;
			Z = z;
			SizeX = sizeX;
			SizeY = sizeY;
			SizeZ = sizeZ;
			CompressedSize = compressedSize;
			CompressedData = compressedData;
		}
	}
}
