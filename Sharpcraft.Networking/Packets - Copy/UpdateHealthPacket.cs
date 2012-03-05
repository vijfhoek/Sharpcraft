using System;
using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class UpdateHealthPacket : Packet
	{
		public Int16 Health;
		public Int16 Food;
		public float Saturation;

		public UpdateHealthPacket(short health = 0, short food = 0, float saturation = 0.0f) : base(PacketType.UpdateHealth)
		{
			Health = health;
			Food = food;
			Saturation = saturation;
		}
	}
}
