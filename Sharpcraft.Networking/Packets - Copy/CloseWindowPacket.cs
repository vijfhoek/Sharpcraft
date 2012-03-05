using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class CloseWindowPacket : Packet
	{
		public sbyte WindowID;

		public CloseWindowPacket(sbyte windowId) : base(PacketType.CloseWindow)
		{
			WindowID = windowId;
		}
	}
}
