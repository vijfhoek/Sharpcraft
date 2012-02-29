using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class IncrementStatisticPacket : Packet
	{
		public Int32 ID;
		public sbyte Amount;

		public IncrementStatisticPacket(Int32 id = 0, sbyte amount = 0) : base(PacketType.IncrementStatistic)
		{
			ID = id;
			Amount = amount;
		}
	}
}
