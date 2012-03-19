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
	/// <summary>
	/// LoginRequest packet sent by the server to the client
	/// after a login request has been accepted.
	/// </summary>
	/// <remarks>If login request is denied a <see cref="DisconnectKickPacket" /> is sent instead.</remarks>
	public class LoginRequestPacketSC : Packet
	{
		/// <summary>
		/// Entity ID of the player.
		/// </summary>
		public int EntityID;

		/// <summary>
		/// Not used.
		/// </summary>
		[Obsolete("Not Used")]
		public string NotUsed;

		/// <summary>
		/// Level type.
		/// </summary>
		/// <remarks><c>"default"</c> or <c>"SUPERFLAT"</c>.</remarks>
		public string LevelType;

		/// <summary>
		/// Game mode on the server.
		/// </summary>
		/// <remarks>0 = Survival, 1 = Creative.</remarks>
		public int Gamemode;

		/// <summary>
		/// Current dimension on the server.
		/// </summary>
		/// <remarks>-1 = Nether, 0 = Overworld, 1 = The End.</remarks>
		public int Dimension;

		/// <summary>
		/// Server difficulty.
		/// </summary>
		/// <remarks>0 = Peaceful, 1 = Easy, 2 = Medium, 3 = Hard.</remarks>
		public sbyte Difficulty;

		/// <summary>
		/// Not used.
		/// </summary>
		/// <remarks>Previously World Height.</remarks>
		[Obsolete("World Height is not sent in LoginRequest packet anymore.")]
		public byte WorldHeight; // NOT USED

		/// <summary>
		/// Max players the server can hold.
		/// </summary>
		public byte MaxPlayers;

		/// <summary>
		/// Initialize a new <see cref="LoginRequestPacketSC" />.
		/// </summary>
		/// <param name="entityId">Entity ID of the player.</param>
		/// <param name="notUsed">Not Used.</param>
		/// <param name="levelType">Level type on the server.</param>
		/// <param name="gamemode">Game mode on the server.</param>
		/// <param name="dimension">Current dimension on the server.</param>
		/// <param name="difficulty">Difficulty on the server.</param>
		/// <param name="worldHeight">Not Used.</param>
		/// <param name="maxPlayers">Max players the server can hold.</param>
		public LoginRequestPacketSC(Int32 entityId = 0, string notUsed = "", string levelType = null, Int32 gamemode = 0,
			Int32 dimension = 0, sbyte difficulty = 0, byte worldHeight = 0, byte maxPlayers = 0) : base(PacketType.LoginRequest)
		{
			EntityID = entityId;
			NotUsed = notUsed;
			LevelType = levelType;
			Gamemode = gamemode;
			Dimension = dimension;
			Difficulty = difficulty;
			WorldHeight = worldHeight;
			MaxPlayers = maxPlayers;
		}
	}
}
