using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class HoldingChangePacket : Packet
	{
		public Int16 SlotID;

		public HoldingChangePacket(Int16 slotId = 0) : base(PacketType.HoldingChange)
		{
			SlotID = slotId;
		}
	}
}
