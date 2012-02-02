/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Sharpcraft.Library.Minecraft;

namespace Sharpcraft.Networking
{
	// TODO Write some docs for these functions, even though they are pretty obvious.
	/// <summary>
	/// Provides various networking tools.
	/// </summary>
	internal class NetworkTools
	{
		private readonly NetworkStream _stream;

		public NetworkTools(NetworkStream stream)
		{
			_stream = stream;
		}

		public String ReadString()
		{
			var bteString = new byte[ReadInt16()*2];
			_stream.Read(bteString, 0, bteString.Length);
			return Encoding.BigEndianUnicode.GetString(bteString);
		}

		public Boolean ReadBoolean()
		{
			byte[] bte = {(byte) _stream.ReadByte()};
			return BitConverter.ToBoolean(bte, 0);
		}

		public Byte ReadByte()
		{
			return (byte) _stream.ReadByte();
		}

		public SByte ReadSignedByte()
		{
			return (sbyte) _stream.ReadByte();
		}

		public Int16 ReadInt16()
		{
			var bte = new byte[2];
			_stream.Read(bte, 0, bte.Length);
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bte, 0));
		}

		public Int32 ReadInt32()
		{
			var bte = new byte[4];
			_stream.Read(bte, 0, bte.Length);
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bte, 0));
		}

		public Int64 ReadInt64()
		{
			var bte = new byte[8];
			_stream.Read(bte, 0, bte.Length);
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bte, 0));
		}

		public Single ReadSingle()
		{
			var bte = new byte[32];
			_stream.Read(bte, 0, bte.Length);
			return BitConverter.ToSingle(bte, 0);
		}

		public Double ReadDouble()
		{
			var bte = new byte[64];
			_stream.Read(bte, 0, bte.Length);
			return BitConverter.ToDouble(bte, 0);
		}

		public ItemStack ReadItemStack()
		{
			ItemStack itemStack = null;
			var itemID = ReadInt16();
			if (itemID >= 0)
			{
				var stackSize = ReadByte();
				var itemDamage = ReadInt16();
				itemStack = new ItemStack(itemID, stackSize, itemDamage);
				// TODO check damageability
			}
			return itemStack;
		}

		public void WriteString(String s)
		{
			WriteInt16((Int16)s.Length);
			var byteString = Encoding.BigEndianUnicode.GetBytes(s);
			_stream.Write(byteString, 0, byteString.Length);
		}

		public void WriteBoolean(Boolean b)
		{
			_stream.WriteByte(Convert.ToByte(b));
		}

		public void WriteByte(Byte i)
		{
			_stream.WriteByte(i);
		}

		public void WriteInt16(Int16 i)
		{
			var bte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
			_stream.Write(bte, 0, bte.Length);
		}

		public void WriteInt32(Int32 i)
		{
			var bte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
			_stream.Write(bte, 0, bte.Length);
		}

		public void WriteInt64(Int64 i)
		{
			var bte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
			_stream.Write(bte, 0, bte.Length);
		}

		public void WriteSingle(Single s)
		{
			var bte = BitConverter.GetBytes(s);
			_stream.Write(bte, 0, bte.Length);
		}

		public void WriteDouble(Double d)
		{
			var bte = BitConverter.GetBytes(d);
			_stream.Write(bte, 0, bte.Length);
		}

		public void Skip(int amount = 1)
		{
			for (var i = 0; i < amount; i++)
				_stream.ReadByte();
		}
	}
}
