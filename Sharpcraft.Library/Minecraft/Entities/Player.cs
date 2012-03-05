/*
 * Player.cs
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

using Microsoft.Xna.Framework.Graphics;

namespace Sharpcraft.Library.Minecraft.Entities
{
	/// <summary>
	/// A minecraft player.
	/// </summary>
	public class Player : Entity
	{
		/// <summary>
		/// Required width on skin.
		/// </summary>
		private const int SkinWidth = 64;

		/// <summary>
		/// Required height on skin.
		/// </summary>
		private const int SkinHeight = 32;

		/// <summary>
		/// The name of the player.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// World position of the player.
		/// </summary>
		public Position Position { get; private set; }

		/// <summary>
		/// The direction this player is looking in.
		/// </summary>
		public LookDirection Direction { get; private set; }

		/// <summary>
		/// Current stance of this player.
		/// </summary>
		public double Stance { get; private set; }

		/// <summary>
		/// Whether or not this player is currently on the ground (not falling).
		/// </summary>
		public bool OnGround { get; private set; }

		/// <summary>
		/// Skin used by this player.
		/// </summary>
		private readonly Skin _skin;

		/// <summary>
		/// Initialize a new instance of <see cref="Player" />.
		/// </summary>
		/// <param name="entityId">The Entity ID of this player.</param>
		/// <param name="name">Name of the player.</param>
		/// <param name="skin">The player skin.</param>
		/// <param name="position">World position of the player (X,Y,Z).</param>
		/// <param name="direction">The direction the player is looking in (Yaw, Pitch).</param>
		/// <param name="stance">The stance of the player.</param>
		public Player(int entityId, string name, Texture2D skin = null, Position position = null, LookDirection direction = null, double stance = 0.0)
			: base(entityId)
		{
			Name = name;
			_skin = new Skin(skin);
			Position = position ?? new Position();
			Direction = direction ?? new LookDirection();
			Stance = stance;
			if (_skin.GetTexture() == null)
				return;
			if (_skin.GetTexture().Width != SkinWidth || _skin.GetTexture().Height != SkinHeight)
			{
				// Set the skin to default player skin (Steve)
			}
		}

		/// <summary>
		/// Updates the world position of this player.
		/// </summary>
		/// <param name="x">X position.</param>
		/// <param name="y">Y position.</param>
		/// <param name="z">Z position (height).</param>
		public void SetPosition(double x, double y, double z)
		{
			SetPosition(new Position(x, y, z));
		}

		/// <summary>
		/// Updates the world position of this player.
		/// </summary>
		/// <param name="position"><c>Vector3</c> with XYZ position of player.</param>
		public void SetPosition(Position position)
		{
			Position = position;
		}

		/// <summary>
		/// Set the direction in which this player is heading.
		/// </summary>
		/// <param name="yaw">Yaw.</param>
		/// <param name="pitch">Pitch.</param>
		public void SetDirection(float yaw, float pitch)
		{
			SetDirection(new LookDirection(yaw, pitch));
		}

		/// <summary>
		/// Set the direction in which this player is heading.
		/// </summary>
		/// <param name="direction"><see cref="Direction" /> object defining the direction.</param>
		public void SetDirection(LookDirection direction)
		{
			Direction = direction;
		}

		/// <summary>
		/// Set this player's stance.
		/// </summary>
		/// <param name="stance"></param>
		public void SetStance(double stance)
		{
			Stance = stance;
		}

		/// <summary>
		/// Set whether or not this player is currently on the ground (not falling).
		/// </summary>
		/// <param name="onGround"><c>true</c> if player is on ground (not falling), <c>false</c> otherwise.</param>
		public void SetOnGround(bool onGround)
		{
			OnGround = onGround;
		}
	}
}
