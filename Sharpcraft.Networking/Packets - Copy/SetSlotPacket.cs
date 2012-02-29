using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class SetSlotPacket : Packet
	{
		public sbyte WindowID;
		public Int16 Slot;
		public object SlotData; // TODO: SlotData: http://wiki.vg/Slot_Data

		public SetSlotPacket(sbyte windowId = 0, Int16 slot = 0, object slotData = null) : base(PacketType.SetSlot)
		{
			WindowID = windowId;
			Slot = slot;
			SlotData = slotData;
		}
	}
}
