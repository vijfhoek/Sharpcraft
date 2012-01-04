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

    public struct Packet1
    {
        public Int32 entityID;
        public Int64 mapSeed;
        public Int32 gamemode;
        public sbyte dimension;
        public sbyte difficulty;
        public byte worldHeight;
        public byte maxPlayers;
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


        public static Int32 GetInt32(byte[] buffer, int offset, EndianType byteOrder)
        {
            if (byteOrder == EndianType.LittleEndian)
                return buffer[offset + 1] << 8 | buffer[offset];
            else
                return buffer[offset] << 8 | buffer[offset + 1];
        }

        public static Int64 GetInt64(byte[] buffer, int offset, EndianType byteOrder)
        {
            if (byteOrder == EndianType.LittleEndian)
                return buffer[offset + 1] << 8 | buffer[offset];
            else
                return buffer[offset] << 8 | buffer[offset + 1];
        }


        // Packet 0x01
        public Packet1 PacketLoginRequest(int version, string username)
        {
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

            // Check if the response packet ID is 0x01
            if (stream.ReadByte() == 0x01) {
                Packet1 packet1;

                // Get the player entity ID
                byte[] bteEntityId = new byte[4];
                stream.Read(bteEntityId, 0, bteEntityId.Length);
                packet1.entityID = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bteEntityId, 0));

                // Skip ahead 2 bytes
                stream.ReadByte(); stream.ReadByte();

                // Get the map seed
                byte[] bteMapSeed = new byte[8];
                stream.Read(bteMapSeed, 0, bteMapSeed.Length);
                packet1.mapSeed = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(bteMapSeed, 0));

                // Get the gamemode
                byte[] bteGamemode = new byte[8];
                stream.Read(bteGamemode, 0, bteGamemode.Length);
                packet1.gamemode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bteGamemode, 0));

                // Get the dimension
                packet1.dimension = (sbyte)stream.ReadByte();

                // Get the difficulty
                packet1.difficulty = (sbyte)stream.ReadByte();

                // Get the world height
                packet1.worldHeight = (byte)stream.ReadByte();

                // Get the maximum amount of players
                packet1.maxPlayers = (byte)stream.ReadByte();

                return packet1;
            }

            return new Packet1();
        }

        // Packet 0x02
        public string PacketHandshake(string username)
        {
            { // C->S part
                // Send the packet ID
                stream.WriteByte(0x02);

                // Write the username
                stream.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)username.Length)), 0, 2);
                byte[] str = Encoding.BigEndianUnicode.GetBytes(username);
                stream.Write(str, 0, str.Length);

                // Flush the stream
                stream.Flush();
            }

            { // S->C part
                // Read the packet ID
                if (stream.ReadByte() != 0x02) return null;
                
                // Read the ID hash
                byte[] bteSize = new byte[2];
                stream.Read(bteSize, 0, bteSize.Length);
                string hash = "";
                for (short s = 0; s < IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bteSize, 0)); s++)
                {
                    byte[] bteChar = new byte[2];
                    stream.Read(bteChar, 0, bteChar.Length);
                    char chr = Encoding.BigEndianUnicode.GetChars(bteChar)[0];
                    hash += chr;
                }

                return hash;
            }
        }

    }
}
