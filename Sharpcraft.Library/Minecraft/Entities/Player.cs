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
		private Vector3 _position;

		/// <summary>
		/// Skin used by this player.
		/// </summary>
		private Skin _skin;

		/// <summary>
		/// Initialize a new instance of <see cref="Player" />.
		/// </summary>
		/// <param name="name">Name of the player.</param>
		/// <param name="skin">The player skin.</param>
		/// <param name="position">World position of the player (X,Y,Z).</param>
		public Player(int entityId, string name, Texture2D skin = null, Vector3 position = new Vector3())
			: base(entityId)
		{
			Name = name;
			_skin = new Skin(skin);
			_position = position;
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
		public void UpdatePosition(float x, float y, float z)
		{
			UpdatePosition(new Vector3(x, y, z));
		}

		/// <summary>
		/// Updates the world position of this player.
		/// </summary>
		/// <param name="position"><c>Vector3</c> with XYZ position of player.</param>
		public void UpdatePosition(Vector3 position)
		{
			_position = position;
		}
	}
}
