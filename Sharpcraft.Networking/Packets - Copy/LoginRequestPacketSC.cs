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
