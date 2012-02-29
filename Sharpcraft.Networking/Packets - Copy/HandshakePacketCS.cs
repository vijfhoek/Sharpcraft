using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class HandshakePacketCS : Packet
	{
		public string Username;

		public HandshakePacketCS(string username = null) : base(PacketType.Handshake)
		{
			Username = username;
		}
	}
}
