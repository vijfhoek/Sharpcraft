using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityMetadataPacket : Packet
	{
		public Int32 EntityID;
		public sbyte[] Metadata; // TODO: Implement Metadata class! http://wiki.vg/Entities
		                         // NOTE: Using an byte array for now, change later!

		public EntityMetadataPacket(Int32 entityId = 0, sbyte[] metadata = null) : base(PacketType.EntityMetadata)
		{
			EntityID = entityId;
			Metadata = metadata;
		}
	}
}
