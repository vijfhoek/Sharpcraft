using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class TransactionPacket : Packet
	{
		public sbyte WindowID;
		public Int16 Action;
		public bool Accepted;

		public TransactionPacket(sbyte windowId = 0, Int16 action = 0, bool accepted = false) : base(PacketType.Transaction)
		{
			WindowID = windowId;
			Action = action;
			Accepted = accepted;
		}
	}
}
