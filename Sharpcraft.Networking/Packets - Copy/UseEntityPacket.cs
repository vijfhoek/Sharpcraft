using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class UseEntityPacket : Packet
	{
		public Int32 AttackerID;
		public Int32 TargetID;
		public bool IsLeftClick;

		public UseEntityPacket(Int32 attackerId = 0, Int32 targetId = 0, bool isLeftClick = false) : base(PacketType.UseEntity)
		{
			AttackerID = attackerId;
			TargetID = targetId;
			IsLeftClick = isLeftClick;
		}
	}
}
