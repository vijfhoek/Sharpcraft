using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace Sharpcraft
{
    public enum EndianType
    {
        LittleEndian,
        BigEndian
    }

    class Protocol
    {
        TcpClient client = null;
        NetworkStream stream = null;

        public Protocol(string server, int port)
        {
            client = new TcpClient(server, port);
            stream = client.GetStream();
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

        
        public bool Packet1LoginRequest(int version, string username)
        {
            byte[] packetID = { 0x01, 0x00, 0x00 };
            stream.Write(packetID, 0, packetID.Length);

            int beVersion = IPAddress.HostToNetworkOrder(version);
            byte[] bteVersion = BitConverter.GetBytes(beVersion);
            stream.Write(bteVersion, 0, bteVersion.Length);

            byte[] bteUsername = Encoding.BigEndianUnicode.GetBytes(username);
            stream.Write(bteUsername, 0, bteUsername.Length);
        }

        public bool Packet2Handshake(string nickname)
        {
            {
                byte[] packetID = { 0x02, 0x00, 0x08 };
                stream.Write(packetID, 0, packetID.Length);

                byte[] packet = Encoding.BigEndianUnicode.GetBytes(nickname);
                stream.Write(packet, 0, packet.Length);
            }

            {
                byte[] packetID = new byte[3];
                stream.Read(packetID, 0, packetID.Length);
                if (packetID[0] != 0x02) return false;

                byte[] packet = new byte[2];
                stream.Read(packet, 0, packet.Length);
                if (Encoding.BigEndianUnicode.GetString(packet) != "-") return false;
            }

            return true;
        }

    }
}
