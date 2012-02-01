using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class UseBedPacket : Packet
	{
		public Int32 EntityID;
		public sbyte InBed; // ????: http://wiki.vg/Protocol#Use_Bed_.280x11.29
		public Int32 X;
		public sbyte Y;
		public Int32 Z;

		public UseBedPacket(Int32 entityId = 0, sbyte inBed = 0, Int32 x = 0, sbyte y = 0, Int32 z = 0) : base(PacketType.UseBed)
		{
			EntityID = entityId;
			InBed = inBed;
			X = x;
			Y = y;
			Z = z;
		}
	}
}
