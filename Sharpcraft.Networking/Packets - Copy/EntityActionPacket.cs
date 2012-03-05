using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityActionPacket : Packet
	{
		public Int32 EntityID;
		public sbyte ActionID;

		public EntityActionPacket(Int32 entityId = 0, sbyte actionId = 0) : base(PacketType.EntityAction)
		{
			EntityID = entityId;
			ActionID = actionId;
		}
	}
}
