/*
 * Skin.cs
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
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

namespace Sharpcraft.Library.Minecraft
{
	// TODO: Also use Skin class for mobs?
	/// <summary>
	/// A skin, used by players.
	/// </summary>
	public class Skin
	{
		/// <summary>
		/// The skin texture.
		/// </summary>
		private Texture2D _texture;

		/// <summary>
		/// Intitialize a new <see cref="Skin" /> class.
		/// </summary>
		/// <param name="texture">The texture object of the skin.</param>
		public Skin(Texture2D texture)
		{
			_texture = texture;
		}

		/// <summary>
		/// Gets the <c>Texture2D</c> object of the skin.
		/// </summary>
		/// <returns></returns>
		public Texture2D GetTexture()
		{
			return _texture;
		}
	}
}
