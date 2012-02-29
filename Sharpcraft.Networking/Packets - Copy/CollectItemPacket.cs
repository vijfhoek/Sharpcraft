using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class CollectItemPacket : Packet
	{
		public Int32 CollectedEntityID;
		public Int32 CollectorEntityID;

		public CollectItemPacket(Int32 collectedEntityId = 0, Int32 collectorEntityId = 0) : base(PacketType.CollectItem)
		{
			CollectedEntityID = collectedEntityId;
			CollectorEntityID = collectorEntityId;
		}
	}
}
