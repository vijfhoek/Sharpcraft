using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PlayerPositionAndLookPacket : Packet
	{
		public double X;
		public double Y;
		public double Z;
		public double Stance;
		public float Yaw;
		public float Pitch;
		public bool OnGround;

		public PlayerPositionAndLookPacket(double x = 0.0, double y = 0.0, double stance = 0.0, double z = 0.0,
			float yaw = 0.0f, float pitch = 0.0f, bool onGround = false) : base(PacketType.PlayerPositionAndLook)
		{
			X = x;
			Y = y;
			Z = z;
			Stance = stance;
			Yaw = yaw;
			Pitch = pitch;
			OnGround = onGround;
		}
	}
}
