using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class DestroyEntityPacket : Packet
	{
		public Int32 EntityID;

		public DestroyEntityPacket(Int32 entityId) : base(PacketType.DestroyEntity)
		{
			EntityID = entityId;
		}
	}
}
