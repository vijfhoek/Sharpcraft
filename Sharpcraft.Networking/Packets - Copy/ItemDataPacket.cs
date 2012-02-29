using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class ItemDataPacket : Packet
	{
		public Int16 ItemType;
		public Int16 ItemID;
		public byte Length;
		public sbyte[] Text;

		public ItemDataPacket(Int16 itemType = 0, Int16 itemId = 0, byte length = 0, sbyte[] text = null) : base(PacketType.ItemData)
		{
			ItemType = itemType;
			ItemID = itemId;
			Length = length;
			Text = text;
		}
	}
}
