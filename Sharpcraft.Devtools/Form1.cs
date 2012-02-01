using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Sharpcraft.Library.Minecraft;
using Newtonsoft.Json;

namespace Sharpcraft.Devtools
{
	public partial class Form1 : Form
	{
		private List<Item> _itemList;

		public Form1()
		{
			InitializeComponent();
			_itemList = new List<Item>();
			//listBox1.DataSource = _itemList;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			listView1.Items.Clear();
			string name = textBox1.Text;
			int id = int.Parse(textBox2.Text);
			int data = int.Parse(textBox3.Text);
			var item = new Item(name, id, data);
			_itemList.Add(item);
		}

		private static int Compare(Item a, Item b)
		{
			if (a.Id == b.Id)
				return a.Data.CompareTo(b.Data);
			return a.Id.CompareTo(b.Id);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_itemList.Sort(Compare);
			foreach (var item in _itemList)
			{
				listView1.Items.Add(new ListViewItem(new[] {item.Id.ToString(), item.Data.ToString(), item.Name}));
			}
			using (var writer = new StreamWriter("items.list", false))
			{
				new JsonSerializer().Serialize(new JsonTextWriter(writer){Formatting = Formatting.Indented}, _itemList);
			}
		}
	}
}
