using System;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Sharpcraft
{
    public enum EndianType
    {
        LittleEndian,
        BigEndian
    }

    class Protocol
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;

        public Protocol(string server, int port)
        {
            try
            {
                _client = new TcpClient(server, port);
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect, details:");
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        // Packet 0x00
        public void PacketKeepAlive(int id)
        {
            // Send PacketID
            _stream.WriteByte(0x00);

            // Send the Keep-alive ID
            byte[] response = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(id));
            _stream.Write(response, 0, response.Length);
        }

        // Packet 0x01
        public bool PacketLoginRequest(int version, string username)
        {
            byte[] packetID = { 0x01, 0x00, 0x00 };
            _stream.Write(packetID, 0, packetID.Length);

            int beVersion = IPAddress.HostToNetworkOrder(version);
            byte[] bteVersion = BitConverter.GetBytes(beVersion);
            _stream.Write(bteVersion, 0, bteVersion.Length);

            byte[] bteUsername = Encoding.BigEndianUnicode.GetBytes(username);
            _stream.Write(bteUsername, 0, bteUsername.Length);

            return true;
        }

        // Packet 0x02
        public bool PacketHandshake(string nickname)
        {
            // NOTE: Why braces here? We can do without...

            byte[] packetID = { 0x02, 0x00, 0x08 };
            _stream.Write(packetID, 0, packetID.Length);

            byte[] packet = Encoding.BigEndianUnicode.GetBytes(nickname);
            _stream.Write(packet, 0, packet.Length);

            packetID = new byte[3];
            _stream.Read(packetID, 0, packetID.Length);
            if (packetID[0] != 0x02)
                return false;

            packet = new byte[2];
            _stream.Read(packet, 0, packet.Length);
            if (Encoding.BigEndianUnicode.GetString(packet) != "-")
                return false;

            return true;
        }
    }
}