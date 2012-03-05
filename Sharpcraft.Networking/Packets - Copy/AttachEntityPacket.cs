using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class AttachEntityPacket : Packet
	{
		public Int32 EntityID;
		public Int32 VehicleID;

		public AttachEntityPacket(Int32 entityId = 0, Int32 vehicleId = 0) : base(PacketType.AttachEntity)
		{
			EntityID = entityId;
			VehicleID = vehicleId;
		}
	}
}
