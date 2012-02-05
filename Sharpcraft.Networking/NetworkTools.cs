/* 
 * Sharpcraft.Protocol
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LibNbt;
using LibNbt.Tags;

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

		public string ReadString()
		{
			Int16 length = ReadInt16();
			Logging.LogManager.GetLogger(this).Debug("ReadString reading string with length: " + length);
			Logging.LogManager.GetLogger(this).Debug("Final: " + (length * 2 + 2));
			var bteString = new byte[length * 2 + 2];
			_stream.Read(bteString, 0, bteString.Length);
			return Encoding.BigEndianUnicode.GetString(bteString);
		}

		public bool ReadBoolean()
		{
			byte[] bte = {(byte) _stream.ReadByte()};
			return BitConverter.ToBoolean(bte, 0);
		}

		public byte ReadByte()
		{
			return (byte) _stream.ReadByte();
		}

		public sbyte ReadSignedByte()
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
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bte, 0));
		}

		public Int64 ReadInt64()
		{
			var bte = new byte[8];
			_stream.Read(bte, 0, bte.Length);
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(bte, 0));
		}

		public Single ReadSingle()
		{
			var bte = new byte[32];
			_stream.Read(bte, 0, bte.Length);
			return BitConverter.ToSingle(bte, 0);
		}

		public double ReadDouble()
		{
			var bte = new byte[64];
			_stream.Read(bte, 0, bte.Length);
			return BitConverter.ToDouble(bte, 0);
		}

		public SlotData ReadSlotData()
		{
			var slotData = new SlotData(ReadInt16());
			if (slotData.ItemID == -1) return null;

			slotData.ItemCount = ReadByte();
			slotData.ItemDamage = ReadInt16();

			var size = ReadInt16();
			if (size == -1) return slotData;
			var data = new byte[size];

			using (var decStream = new GZipStream(_stream, CompressionMode.Decompress)) decStream.Read(data, 0, data.Length);
			using (var memStream = new MemoryStream(size))
			{
				memStream.Write(data, 0, data.Length);
				var file = new NbtFile(); file.LoadFile(memStream, false);
				var list = file.Query<NbtCompound>("").Query<NbtList>("ench");
				slotData.ItemEnchantments = list;
			}

			return slotData;
		}

		/*public ItemStack ReadItemStack()
		{
			ItemStack itemStack = null;
			var itemID = ReadInt16();
			if (itemID >= 0)
			{
				var stackSize = ReadByte();
				var itemDamage = ReadInt16();
				// TODO: Pass an actual item instead of null
				itemStack = new ItemStack(null, stackSize, itemDamage);
				//if (Item.)
			}
			return itemStack;
		}*/

		public void WriteString(string s)
		{
			WriteInt16((Int16)s.Length);
			var byteString = Encoding.BigEndianUnicode.GetBytes(s);
			_stream.Write(byteString, 0, byteString.Length);
		}

		public void WriteBoolean(bool b)
		{
			_stream.WriteByte(Convert.ToByte(b));
		}

		public void WriteByte(byte i)
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

		public void WriteDouble(double d)
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
