using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class NewInvalidStatePacket : Packet
	{
		public sbyte Reason;
		public sbyte GameMode;

		public NewInvalidStatePacket(sbyte reason = 0, sbyte gameMode = 0) : base(PacketType.NewInvalidState)
		{
			Reason = reason;
			GameMode = gameMode;
		}
	}
}
