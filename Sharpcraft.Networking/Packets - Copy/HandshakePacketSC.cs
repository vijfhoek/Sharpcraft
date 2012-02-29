using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class HandshakePacketSC : Packet
	{
		public string ConnectionHash;

		public HandshakePacketSC(string connectionHash = null) : base(PacketType.Handshake)
		{
			ConnectionHash = connectionHash;
		}
	}
}
