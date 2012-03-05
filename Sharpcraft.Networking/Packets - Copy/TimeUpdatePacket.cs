using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class TimeUpdatePacket : Packet
	{
		public Int64 Time;

		public TimeUpdatePacket(Int64 time = 0) : base(PacketType.TimeUpdate)
		{
			Time = time;
		}
	}
}
