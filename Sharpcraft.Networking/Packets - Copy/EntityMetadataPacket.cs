using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityMetadataPacket : Packet
	{
		public Int32 EntityID;
		public object Metadata; // TODO: Implement Metadata class! http://wiki.vg/Entities

		public EntityMetadataPacket(Int32 entityId = 0, object metadata = null) : base(PacketType.EntityMetadata)
		{
			EntityID = entityId;
			Metadata = metadata;
		}
	}
}
