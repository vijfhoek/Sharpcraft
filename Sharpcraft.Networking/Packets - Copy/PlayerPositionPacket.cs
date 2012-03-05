using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PlayerPositionPacket : Packet
	{
		public double X;
		public double Y;
		public double Z;
		public double Stance;
		public bool OnGround;

		public PlayerPositionPacket(double x = 0.0, double y = 0.0, double stance = 0.0, double z = 0.0, bool onGround = false) : base(PacketType.PlayerPosition)
		{
			X = x;
			Y = y;
			Z = z;
			Stance = stance;
			OnGround = onGround;
		}
	}
}
