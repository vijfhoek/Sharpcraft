/*
 * LoginRequestPacketSC.cs
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
	public class LoginRequestPacketSC : Packet
	{
		public Int32 EntityID;
		public string NotUsed;
		public Int64 MapSeed;
		public string LevelType;
		public Int32 Gamemode;
		public sbyte Dimension;
		public sbyte Difficulty;
		public byte WorldHeight;
		public byte MaxPlayers;

		public LoginRequestPacketSC(Int32 entityId = 0, Int64 mapSeed = 0, string levelType = null, Int32 gamemode = 0,
			sbyte dimension = 0, sbyte difficulty = 0, byte worldHeight = 0, byte maxPlayers = 0) : base(PacketType.LoginRequest)
		{
			EntityID = entityId;
			MapSeed = mapSeed;
			Gamemode = gamemode;
			Dimension = dimension;
			Difficulty = difficulty;
			WorldHeight = worldHeight;
			MaxPlayers = maxPlayers;
		}
	}
}
