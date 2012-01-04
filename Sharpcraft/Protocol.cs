using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace Sharpcraft
{
	public enum EndianType
	{
		LittleEndian,
		BigEndian
	}

	public class Protocol
	{
		TcpClient client = new TcpClient();
		NetworkStream stream = null;

		public Protocol(string server, int port)
		{
			client.Connect(server, port);
			Thread.Sleep(256);
			stream = client.GetStream();
			Thread.Sleep(512);
		}


		public byte[] StringToBytes(string str)
		{
			List<Byte> bytes = new List<byte>();

			byte[] strLength = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(str.Length));
			foreach (byte bte in strLength)
				bytes.Add(bte);

			byte[] bteString = Encoding.BigEndianUnicode.GetBytes(str);
			foreach (byte bte in bteString)
				bytes.Add(bte);

			return bytes.ToArray();
		}

		public string BytesToString(byte[] bytes) {
			byte[] bteStrLength = { bytes[0], bytes[1] };
			int strLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bteStrLength, 0));

			string str = "";

			for (short s = 1; s < strLength + 1; s++)
			{
				byte[] tmp = { bytes[s * 2], bytes[(s * 2) + 1] };
				str += Encoding.BigEndianUnicode.GetString(tmp);
			}

			return str;
		}


		public Packet GetPacket()
		{
			byte packetID = (byte)stream.ReadByte();
			Packet pack = null;

			if (packetID == 0x00) // Keep alive
			{
				Packet0 packet = new Packet0();
				packet.packetID = 0x00;

				byte[] bteKeepAliveID = new byte[4];
				stream.Read(bteKeepAliveID, 0, bteKeepAliveID.Length);
				packet.keepAliveID = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bteKeepAliveID, 0));

				pack = (Packet)packet;
			}
			else if (packetID == 0x01) // Login Request
			{
				Packet1 packet = new Packet1();
				packet.packetID = 0x01;

				// Get the player entity ID
				byte[] bteEntityId = new byte[4];
				stream.Read(bteEntityId, 0, bteEntityId.Length);
				packet.entityID = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bteEntityId, 0));

				// Skip ahead 2 bytes
				stream.ReadByte(); stream.ReadByte();

				// Get the map seed
				byte[] bteMapSeed = new byte[8];
				stream.Read(bteMapSeed, 0, bteMapSeed.Length);
				packet.mapSeed = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(bteMapSeed, 0));

				// Get the gamemode
				byte[] bteGamemode = new byte[4];
				stream.Read(bteGamemode, 0, bteGamemode.Length);
				packet.gamemode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bteGamemode, 0));

				// Get the dimension
				packet.dimension = (sbyte)stream.ReadByte();

				// Get the difficulty
				packet.difficulty = (sbyte)stream.ReadByte();

				// Get the world height
				packet.worldHeight = (byte)stream.ReadByte();

				// Get the maximum amount of players
				packet.maxPlayers = (byte)stream.ReadByte();

				pack = (Packet)packet;
			}
			else if (packetID == 0x02)
			{
				Packet2 packet = new Packet2();
				packet.packetID = 0x02;

				// Get the connection hash
				byte[] bteConnectionHashLength = { (byte)stream.ReadByte(), (byte)stream.ReadByte() };
				short connectionHashLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bteConnectionHashLength, 0));
				for (short s = 0; s < connectionHashLength; s++)
				{
					byte[] bte = { (byte)stream.ReadByte(), (byte)stream.ReadByte() };
					packet.connectionHash += Encoding.BigEndianUnicode.GetString(bte);
				}
			}

			return pack;
		}
			
		// Packet 0x01
		public void PacketLoginRequest(int version, string username)
		{
			// Write the Packet ID (0x01)
			stream.WriteByte(0x01);

			// Write the protocol version (22 (0x16) for 1.0.0)
			int beVersion = IPAddress.HostToNetworkOrder(version);
			byte[] bteVersion = BitConverter.GetBytes(beVersion);
			stream.Write(bteVersion, 0, bteVersion.Length);

			// Write the username
			stream.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)username.Length)), 0, 2);
			byte[] bteUsername = Encoding.BigEndianUnicode.GetBytes(username);
			stream.Write(bteUsername, 0, bteUsername.Length);

			// Write NotUsed 1
			byte[] bteNotUsed1 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
			stream.Write(bteNotUsed1, 0, bteNotUsed1.Length);

			// Write NotUsed 2
			byte[] bteNotUsed2 = { 0x00, 0x00, 0x00, 0x00 };
			stream.Write(bteNotUsed2, 0, bteNotUsed2.Length);

			// Write NotUsed 3 through 6
			stream.WriteByte(0x00);
			stream.WriteByte(0x00);
			stream.WriteByte(0x00);
			stream.WriteByte(0x00);

			// Flush the stream
			stream.Flush();
		}

		// Packet 0x02
		public void PacketHandshake(string username)
		{
			// Send the packet ID
			stream.WriteByte(0x02);

			// Write the username
			stream.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)username.Length)), 0, 2);
			byte[] str = Encoding.BigEndianUnicode.GetBytes(username);
			stream.Write(str, 0, str.Length);

			// Flush the stream
			stream.Flush();
		}

	}
}
