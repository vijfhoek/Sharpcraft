using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class PluginMessagePacket : Packet
	{
		public string Channel;
		public Int16 Length;
		public sbyte[] Data;

		public PluginMessagePacket(string channel = null, Int16 length = 0, sbyte[] data = null)
			: base(PacketType.PluginMessage)
		{
			Channel = channel;
			Length = length;
			Data = data;
		}
	}
}
