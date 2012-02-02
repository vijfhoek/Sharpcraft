using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class Packet
	{
		public PacketType Type;

		public Packet(PacketType type)
		{
			Type = type;
		}
	}
}
