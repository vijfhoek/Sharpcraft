using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	//TODO: Finish implementation of HeldItem / SlotData
	public class PlayerBlockPlacementPacket : Packet
	{
		public Int32 X;
		public sbyte Y;
		public Int32 Z;
		public sbyte Direction;
		public object HeldItem; // TODO: This is supposed to be SlotData: http://wiki.vg/Slot_Data

		public PlayerBlockPlacementPacket(Int32 x = 0, sbyte y = 0, Int32 z = 0, sbyte direction = 0, object heldItem = null) : base(PacketType.PlayerBlockPlacement)
		{
			X = x;
			Y = y;
			Z = z;
			Direction = direction;
			HeldItem = heldItem;
		}
	}
}
