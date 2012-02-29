using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class WindowClickPacket : Packet
	{
		public sbyte WindowID;
		public Int16 Slot;
		public sbyte RightClick;
		public Int16 ActionNumber;
		public bool Shift;
		public object ClickedItem; // TODO: SlotData: http://wiki.vg/Slot_Data

		public WindowClickPacket(sbyte windowId = 0, Int16 slot = 0, sbyte rightClick = 0, Int16 actionNumber = 0, bool shift = false,
			object clickedItem = null) : base(PacketType.WindowClick)
		{
			WindowID = windowId;
			Slot = slot;
			RightClick = rightClick;
			ActionNumber = actionNumber;
			Shift = shift;
			ClickedItem = clickedItem;
		}
	}
}
