using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class UpdateSignPacket : Packet
	{
		public Int32 X;
		public Int16 Y;
		public Int32 Z;
		public string Text1;
		public string Text2;
		public string Text3;
		public string Text4;

		public UpdateSignPacket(Int32 x = 0, Int16 y = 0, Int32 z = 0, string text1 = null, string text2 = null, string text3 = null, string text4 = null)
			: base(PacketType.UpdateSign)
		{
			X = x;
			Y = y;
			Z = z;
			Text1 = text1;
			Text2 = text2;
			Text3 = text3;
			Text4 = text4;
		}
	}
}
