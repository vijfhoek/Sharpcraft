using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Sharpcraft
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class FrameRateCounter : DrawableGameComponent
	{
		private ContentManager _content;
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;

		private int _fps;
		private int _frameCount;
		private TimeSpan _elapsed = TimeSpan.Zero;

		internal bool FpsEnabled;

		internal FrameRateCounter(Game game) : base(game)
		{
			_content = new ContentManager(game.Services, "content");
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = _content.Load<SpriteFont>("Font");
		}

		protected override void UnloadContent()
		{
			_content.Unload();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			_elapsed += gameTime.ElapsedGameTime;

			if (_elapsed >= TimeSpan.FromSeconds(1))
			{
				_elapsed -= TimeSpan.FromSeconds(1);
				_fps = _frameCount;
				_frameCount = 0;
			}
		}

		public override void Draw(GameTime gameTime)
		{
			_frameCount++;

			if (!FpsEnabled)
				return;

			_spriteBatch.Begin();
			_spriteBatch.DrawString(_font, "FPS: " + _fps, new Vector2(32, 32), Color.Black);
			_spriteBatch.DrawString(_font, "FC:  " + _frameCount, new Vector2(32, 48), Color.Black);
			_spriteBatch.DrawString(_font, "ELA: " + _elapsed, new Vector2(32, 64), Color.Black);
			_spriteBatch.End();
		}
	}
}
