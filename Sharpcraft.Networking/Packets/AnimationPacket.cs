using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class AnimationPacket : Packet
	{
		public Int32 EntityID;
		public sbyte Animation;

		public AnimationPacket(Int32 entityId = 0, sbyte animation = 0) : base(PacketType.Animation)
		{
			EntityID = entityId;
			Animation = animation;
		}
	}
}
