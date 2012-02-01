using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class ChatMessagePacket : Packet
	{
		public string Message;

		public ChatMessagePacket(string message = null) : base(PacketType.ChatMessage)
		{
			Message = message;
		}
	}
}
