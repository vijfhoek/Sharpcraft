using Sharpcraft.Networking.Enums;
using Sharpcraft.Library.Minecraft;

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
