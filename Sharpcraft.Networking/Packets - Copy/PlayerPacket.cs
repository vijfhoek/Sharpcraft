using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PlayerPacket : Packet
	{
		public bool OnGround;

		public PlayerPacket(bool onGround = false) : base(PacketType.Player)
		{
			OnGround = onGround;
		}
	}
}
