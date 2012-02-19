using Microsoft.Xna.Framework;
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
		public Direction Direction { get; private set; }

		public double Stance { get; private set; }

		public bool OnGround { get; private set; }

		/// <summary>
		/// Skin used by this player.
		/// </summary>
		private Skin _skin;

		/// <summary>
		/// Initialize a new instance of <see cref="Player" />.
		/// </summary>
		/// <param name="entityId">The Entity ID of this player.</param>
		/// <param name="name">Name of the player.</param>
		/// <param name="skin">The player skin.</param>
		/// <param name="position">World position of the player (X,Y,Z).</param>
		/// <param name="direction">The direction the player is looking in (Yaw, Pitch).</param>
		/// <param name="stance">The stance of the player.</param>
		public Player(int entityId, string name, Texture2D skin = null, Position position = null, Direction direction = null, double stance = 0.0)
			: base(entityId)
		{
			Name = name;
			_skin = new Skin(skin);
			Position = position ?? new Position();
			Direction = direction ?? new Direction();
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

		public void SetDirection(float yaw, float pitch)
		{
			SetDirection(new Direction(yaw, pitch));
		}

		public void SetDirection(Direction direction)
		{
			Direction = direction;
		}

		public void SetStance(double stance)
		{
			Stance = stance;
		}

		public void SetOnGround(bool onGround)
		{
			OnGround = onGround;
		}
	}
}
