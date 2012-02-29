/*
 * Label.cs
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
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

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
