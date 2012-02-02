using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class ExperienceOrbPacket : Packet
	{
		public Int32 EntityID;
		public Int32 X;
		public Int32 Y;
		public Int32 Z;
		public Int16 Count;

		public ExperienceOrbPacket(Int32 entityId = 0, Int32 x = 0, Int32 y = 0, Int32 z = 0, Int16 count = 0) : base(PacketType.ExperienceOrb)
		{
			EntityID = entityId;
			X = x;
			Y = y;
			Z = z;
			Count = count;
		}
	}
}
