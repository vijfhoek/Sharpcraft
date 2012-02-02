using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class OpenWindowPacket : Packet
	{
		public sbyte WindowID;
		public sbyte InventoryType;
		public string Title;
		public sbyte Slots;

		public OpenWindowPacket(sbyte windowId = 0, sbyte inventoryType = 0, string title = null, sbyte slots = 0)
			: base(PacketType.OpenWindow)
		{
			WindowID = windowId;
			InventoryType = inventoryType;
			Title = title;
			Slots = slots;
		}
	}
}
