using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sharpcraft.Library.GUI
{
	public class Label
	{
		public SpriteFont Font;
		public string Text;
		public Vector2 Size { get { return Font.MeasureString(Text); } }
		public float Width { get { return Size.X; } }
		public float Height { get { return Size.Y; } }
		public Color ForeColor;

		public Label(string text = null, SpriteFont font = null) : this(text, font, Color.Black) { }

		public Label(string text, SpriteFont font, Color foreColor)
		{
			Font = font;
			Text = text;
			ForeColor = foreColor;
		}

		public Vector2 GetCenterPosition(Vector2 source)
		{
			return GetCenterPosition(source, new Vector2(0, 0));
		}

		public Vector2 GetCenterPosition(Vector2 source, Vector2 offset)
		{
			return new Vector2(source.X / 2 - Size.X / 2 + offset.X, source.Y / 2 - Size.Y / 2 + offset.Y);
		}
	}
}
