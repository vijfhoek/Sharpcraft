using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class DisconnectKickPacket : Packet
	{
		public string Reason;

		public DisconnectKickPacket(string reason = null) : base(PacketType.DisconnectKick)
		{
			Reason = reason;
		}
	}
}
