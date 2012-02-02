using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EnchantItemPacket : Packet
	{
		public sbyte WindowID;
		public sbyte Enchantment;

		public EnchantItemPacket(sbyte windowId = 0, sbyte enchantment = 0) : base(PacketType.EnchantItem)
		{
			WindowID = windowId;
			Enchantment = enchantment;
		}
	}
}
