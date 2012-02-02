using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class ExperiencePacket : Packet
	{
		public float Bar;
		public Int16 Level;
		public Int16 Total;

		public ExperiencePacket(float bar = 0.0f, Int16 level = 0, Int16 total = 0) : base(PacketType.Experience)
		{
			Bar = bar;
			Level = level;
			Total = total;
		}
	}
}
