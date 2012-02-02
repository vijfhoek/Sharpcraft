using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class ServerListPingPacket : Packet
	{
		public string Data;

		public ServerListPingPacket(string data = null) : base(PacketType.ServerListPing)
		{
			Data = data;
		}
	}
}
