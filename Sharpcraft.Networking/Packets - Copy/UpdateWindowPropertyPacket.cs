using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class UpdateWindowPropertyPacket : Packet
	{
		public sbyte WindowID;
		public Int16 Property;
		public Int16 Value;

		public UpdateWindowPropertyPacket(sbyte windowId = 0, Int16 property = 0, Int16 value = 0) : base(PacketType.UpdateWindowProperty)
		{
			WindowID = windowId;
			Property = property;
			Value = value;
		}
	}
}
