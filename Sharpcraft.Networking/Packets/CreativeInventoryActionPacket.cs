using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class CreativeInventoryActionPacket : Packet
	{
		public Int16 Slot;
		public SlotData ClickedItem;

		public CreativeInventoryActionPacket(Int16 slot = 0, SlotData clickedItem = null) : base(PacketType.CreativeInventoryAction)
		{
			Slot = slot;
			ClickedItem = clickedItem;
		}
	}
}
