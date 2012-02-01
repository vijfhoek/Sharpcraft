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
