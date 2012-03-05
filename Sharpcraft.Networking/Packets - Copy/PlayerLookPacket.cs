using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PlayerLookPacket : Packet
	{
		public float Yaw;
		public float Pitch;
		public bool OnGround;

		public PlayerLookPacket(float yaw = 0.0f, float pitch = 0.0f, bool onGround = false) : base(PacketType.PlayerLook)
		{
			Yaw = yaw;
			Pitch = pitch;
			OnGround = onGround;
		}
	}
}
