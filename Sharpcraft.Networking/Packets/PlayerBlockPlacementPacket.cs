using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	//TODO: Finish implementation of HeldItem / SlotData
	public class PlayerBlockPlacementPacket : Packet
	{
		public Int32 X;
		public SByte Y;
		public Int32 Z;
		public SByte Direction;
		public ItemStack HeldItem; // NOTE changed to ItemStack instead of SlotDate, seeing that the Notchian client does it that way

		public PlayerBlockPlacementPacket(Int32 x = 0, sbyte y = 0, Int32 z = 0, sbyte direction = 0, ItemStack heldItem = null) : base(PacketType.PlayerBlockPlacement)
		{
			X = x;
			Y = y;
			Z = z;
			Direction = direction;
			HeldItem = heldItem;
		}
	}
}
