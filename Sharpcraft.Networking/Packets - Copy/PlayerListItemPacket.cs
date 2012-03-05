using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PlayerListItemPacket : Packet
	{
		public string PlayerName;
		public bool Online;
		public Int16 Ping;

		public PlayerListItemPacket(string playerName = null, bool online = false, Int16 ping = 0)
			: base(PacketType.PlayerListItem)
		{
			PlayerName = playerName;
			Online = online;
			Ping = ping;
		}
	}
}
