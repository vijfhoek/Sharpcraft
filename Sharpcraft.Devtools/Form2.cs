using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sharpcraft.Devtools
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private void Button1Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				foreach (var file in Directory.GetFiles(folderBrowserDialog1.SelectedPath))
				{
					if (Path.GetExtension(file) == ".cs")
						listBox1.Items.Add(file);
				}
			}
		}

		private void Button2Click(object sender, EventArgs e)
		{
			if (listBox1.Items.Count <= 0)
				return;
			if (string.IsNullOrEmpty(textBox1.Text))
				return;
			label1.Text = "Working...";
			Application.DoEvents();
			int count = 0;
			foreach (string file in listBox1.Items)
			{
				try
				{
					string content = File.ReadAllText(file);
					var writer = new StreamWriter(file, false, Encoding.UTF8);
					string newContent = textBox1.Text;
					newContent = newContent.Replace("$FILENAME$", Path.GetFileName(file));
					newContent = newContent + content;
					writer.Write(newContent);
					writer.Flush();
					writer.Close();
					count++;
					label1.Text = string.Format("{0}/{1} files updated...", count, listBox1.Items.Count);
					Application.DoEvents();
				}
				catch (Exception)
				{
					continue;
				}
			}
		}
	}
}
