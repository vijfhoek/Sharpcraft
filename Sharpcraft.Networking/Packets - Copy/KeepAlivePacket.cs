using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class KeepAlivePacket : Packet
	{
		public Int32 KeepAliveID;

		public KeepAlivePacket(Int32 id = 0) : base(PacketType.KeepAlive)
		{
			KeepAliveID = id;
		}
	}
}
