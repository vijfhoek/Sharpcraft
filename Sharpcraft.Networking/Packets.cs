/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using Sharpcraft.Networking.Enums;
using Sharpcraft.Networking.Packets;

namespace Sharpcraft.Networking
{

	public class DisconnectKickPacket : Packet
	{
		public string Reason;

		public DisconnectKickPacket(string reason) : base(PacketType.DisconnectKick)
		{
			Reason = reason;
		}
	}
}
