using System;

using Sharpcraft.Networking.Enums;

namespace Sharpcraft.Networking.Packets
{
	public class LoginRequestPacketCS : Packet
	{
		public Int32 ProtocolVersion;
		public string Username;

		public LoginRequestPacketCS(Int32 protocolVersion = 0, string username = null) : base(PacketType.LoginRequest)
		{
			ProtocolVersion = protocolVersion;
			Username = username;
		}
	}
}
