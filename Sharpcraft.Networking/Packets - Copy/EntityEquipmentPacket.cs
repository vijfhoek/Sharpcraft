using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class EntityEquipmentPacket : Packet
	{
		public Int32 EntityID;
		public Int16 Slot;
		public Int16 ItemID;
		public Int16 Damage;

		public EntityEquipmentPacket(Int32 entityId = 0, Int16 slot = 0, Int16 itemId = 0, Int16 damage = 0) : base(PacketType.EntityEquipment)
		{
			EntityID = entityId;
			Slot = slot;
			ItemID = itemId;
			Damage = damage;
		}
	}
}
