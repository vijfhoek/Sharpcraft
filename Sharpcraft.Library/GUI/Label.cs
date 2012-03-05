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
 * Sijmen Schoon and Adam Hellberg do not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sharpcraft.Library.GUI
{
	/// <summary>
	/// A text label displayed on screen.
	/// </summary>
	public class Label
	{
		/// <summary>
		/// The font to use for this label.
		/// </summary>
		public SpriteFont Font;

		/// <summary>
		/// The label text.
		/// </summary>
		public string Text;

		/// <summary>
		/// Size of the label, this is calculated automatically
		/// with Font.MeasureString.
		/// </summary>
		public Vector2 Size { get { return Font.MeasureString(Text); } }

		/// <summary>
		/// Width of the label, this is obtained automatically from
		/// <see cref="Size" />.
		/// </summary>
		public float Width { get { return Size.X; } }

		/// <summary>
		/// Height of the label, this is obtained automatically from
		/// <see cref="Size" />.
		/// </summary>
		public float Height { get { return Size.Y; } }

		/// <summary>
		/// Forecolor of this label (I.E: Text color).
		/// </summary>
		public Color ForeColor;

		/// <summary>
		/// Initialize a new label.
		/// </summary>
		/// <param name="text">Text of the label.</param>
		/// <param name="font">Font to use for this label.</param>
		public Label(string text = null, SpriteFont font = null) : this(text, font, Color.Black) { }

		/// <summary>
		/// Initialize a new label.
		/// </summary>
		/// <param name="text">Text of the label.</param>
		/// <param name="font">Font to use for this label.</param>
		/// <param name="foreColor">Forecolor of this label.</param>
		public Label(string text, SpriteFont font, Color foreColor)
		{
			Font = font;
			Text = text;
			ForeColor = foreColor;
		}
		
		/// <summary>
		/// Get center position for this label based on a source rectangle.
		/// </summary>
		/// <param name="source">The source rectangle to get center position for.</param>
		/// <returns><see cref="Vector2" /> specifying center position for this <see cref="Label" />.</returns>
		public Vector2 GetCenterPosition(Vector2 source)
		{
			return GetCenterPosition(source, new Vector2(0, 0));
		}

		/// <summary>
		/// Get center position for this label based on a source rectangle.
		/// </summary>
		/// <param name="source">The source rectangle to get center position for.</param>
		/// <param name="offset"><see cref="Vector2" /> defining X and Y offset for the label position.</param>
		/// <returns><see cref="Vector2" /> specifying center position for this <see cref="Label" /> (with offset applied).</returns>
		public Vector2 GetCenterPosition(Vector2 source, Vector2 offset)
		{
			return new Vector2(source.X / 2 - Size.X / 2 + offset.X, source.Y / 2 - Size.Y / 2 + offset.Y);
		}
	}
}
