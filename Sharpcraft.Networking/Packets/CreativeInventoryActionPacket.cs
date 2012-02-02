using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class CreativeInventoryActionPacket : Packet
	{
		public Int16 Slot;
		public object ClickedItem; // TODO: SlotData: http://wiki.vg/Slot_Data

		public CreativeInventoryActionPacket(Int16 slot = 0, object clickedItem = null) : base(PacketType.CreativeInventoryAction)
		{
			Slot = slot;
			ClickedItem = clickedItem;
		}
	}
}
