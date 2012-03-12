/*
 * NetworkTools.cs
 * 
 * Copyright © 2011-2012 by Sijmen Schoon and Adam Hellberg.
 * 
 * This file is part of Sharpcraft.
 * 
 * Sharpcraft is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Sharpcraft is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Sharpcraft.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Disclaimer: Sharpcraft is in no way affiliated with Mojang AB and/or
 * any of its employees and/or licensors.
 * Sijmen Schoon and Adam Hellberg do not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

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
			try
			{
				var bteString = new byte[ReadInt16() * 2 + 2];
				if (bteString.Length <= 0)
				{
					Logging.LogManager.GetLogger(this).Error("Tried to create BYTE ARRAY with a length of " + bteString.Length + "."
					                                         + " Returning empty string...");
					return string.Empty;
				}
				_stream.Read(bteString, 0, bteString.Length);
				return Encoding.BigEndianUnicode.GetString(bteString);
			}
			catch (OverflowException)
			{
				Logging.LogManager.GetLogger(this).Error("OverflowException occurred in ReadString, returning empty string...");
				return string.Empty;
			}
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

		public short ReadInt16()
		{
			var bte = new byte[2];
			_stream.Read(bte, 0, bte.Length);
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bte, 0));
		}

		public int ReadInt32()
		{
			var bte = new byte[4];
			_stream.Read(bte, 0, bte.Length);
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bte, 0));
		}

		public long ReadInt64()
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

		public byte[] ReadBytes(short length)
		{
			var data = new byte[length];
			_stream.Read(data, 0, data.Length);
			return data;
		}

		public byte[] ReadBytes(int length)
		{
			var data = new byte[length];
			_stream.Read(data, 0, data.Length);
			return data;
		}
		
		public sbyte[] ReadSignedBytes(short length)
		{
			var data = ReadBytes(length);
			var sdata = new sbyte[length];
			
			for (short i = 0; i < length; i++)
				sdata[length] = (sbyte) data.Length;

			return sdata;
		}

		public sbyte[] ReadSignedBytes(int length)
		{
			var data = ReadBytes(length);
			var sdata = new sbyte[length];

			for (var i = 0; i < length; i++)
				sdata[length] = (sbyte)data.Length;

			return sdata;
		}

		public SlotData ReadSlotData()
		{
			var slotData = new SlotData(ReadInt16());
			if (slotData.ItemID == -1) return null;

			slotData.ItemCount = ReadByte();
			slotData.ItemDamage = ReadInt16();

			var size = ReadInt16();
			if (size == -1) return slotData;

			var file = new NbtFile();
			file.LoadFile(_stream, true);
			var list = file.Query<NbtCompound>("").Query<NbtList>("ench");
			slotData.ItemEnchantments = list;

			return slotData;
		}

		public Dictionary<int, object> ReadEntityMetadata()
		{
			var objects = new Dictionary<int, object>();
			var x = ReadByte();

			while (x != 127)
			{
				var index = x & 0x1F;	// Lower 5 bits
				var ty = x >> 5;		// Upper 3 bits
				object val;

				switch (ty)
				{
					case 0:
						val = ReadByte();
						break;
					case 1:
						val = ReadInt16();
						break;
					case 2:
						val = ReadInt32();
						break;
					case 3:
						val = ReadSingle();
						break;
					case 4:
						val = ReadString();
						break;
					case 5:
						{
							var dict = new Dictionary<string, object>();
							dict.Add("id", ReadInt16());
							dict.Add("count", ReadByte());
							dict.Add("damage", ReadInt16());
							val = dict;
						}
						break;
					case 6:
						val = new[] { ReadInt32(), ReadInt32(), ReadInt32() };
						break;
					default:
						val = null;
						break;
				}
				objects.Add(index, val);

				x = ReadByte();
			}

			return objects;
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

		public void WriteBytes(byte[] b)
		{
			_stream.Write(b, 0, b.Length);
		}

		public void Skip(int amount = 1)
		{
			for (var i = 0; i < amount; i++)
				_stream.ReadByte();
		}
	}
}
