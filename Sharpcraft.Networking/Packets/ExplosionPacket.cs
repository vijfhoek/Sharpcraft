using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class ExplosionPacket : Packet
	{
		public double X;
		public double Y;
		public double Z;
		public float Unknown; // Radius?
		public Int32 Count;
		public sbyte[,] Records;

		public ExplosionPacket(double x = 0.0, double y = 0.0, double z = 0.0, float unknown = 0.0f,
			Int32 count = 0, sbyte[,] records = null) : base(PacketType.Explosion)
		{
			X = x;
			Y = y;
			Z = z;
			Unknown = unknown;
			Count = count;
			Records = records;
		}
	}
}
