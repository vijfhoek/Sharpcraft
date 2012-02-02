using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class WindowItemsPacket : Packet
	{
		public sbyte WindowID;
		public Int16 Count;
		public object[] SlotData; // TODO: Array of SlotData: http://wiki.vg/Slot_Data

		public WindowItemsPacket(sbyte windowId = 0, Int16 count = 0, object[] slotData = null) : base(PacketType.WindowItems)
		{
			WindowID = windowId;
			Count = count;
			SlotData = slotData;
		}
	}
}
