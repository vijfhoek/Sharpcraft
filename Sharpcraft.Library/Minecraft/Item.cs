using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharpcraft.Library.Minecraft
{
	public class Item
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		public int Data { get; private set; }

		public Item(string name, int id, int data)
		{
			Name = name;
			Id = id;
			Data = data;
		}
	}
}
