using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Keys = Microsoft.Xna.Framework.Input.Keys;

using Sharpcraft.Logging;

namespace Sharpcraft.Components.Debug
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class FrameRateDisplay : DrawableGameComponent
	{
		private log4net.ILog _log;

		private readonly ContentManager _content;
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;

		private int _fps;
		private int _frameCount;
		private TimeSpan _elapsed = TimeSpan.Zero;

		private bool _fpsToggling;
		private bool _fpsEnabled;

		internal FrameRateDisplay(Game game) : base(game)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("FrameRateDisplay created!");
			_content = new ContentManager(game.Services, SharpcraftConstants.ContentDirectory);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = _content.Load<SpriteFont>(SharpcraftConstants.DebugFont);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			_log.Debug("FrameRateDisplay is unloading!");
			_content.Unload();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.F3) && !_fpsToggling)
			{
				_fpsToggling = true;
				_fpsEnabled = !_fpsEnabled;
			}

			if (Keyboard.GetState().IsKeyUp(Keys.F3))
				_fpsToggling = false;

			_elapsed += gameTime.ElapsedGameTime;

			if (_elapsed >= TimeSpan.FromSeconds(1))
			{
				_elapsed -= TimeSpan.FromSeconds(1);
				_fps = _frameCount;
				_frameCount = 0;
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			_frameCount++;

			if (!_fpsEnabled)
				return;

			_spriteBatch.Begin();
			_spriteBatch.DrawString(_font, "FPS: " + _fps, new Vector2(32, 32), Color.Black);
			_spriteBatch.DrawString(_font, "FC:  " + _frameCount, new Vector2(32, 48), Color.Black);
			_spriteBatch.DrawString(_font, "ELA: " + _elapsed, new Vector2(32, 64), Color.Black);
			_spriteBatch.End();
		}
	}
}
