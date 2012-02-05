using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Keys = Microsoft.Xna.Framework.Input.Keys;

using Sharpcraft.Logging;

namespace Sharpcraft.Components.Debug
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class DebugDisplay : DrawableGameComponent
	{
		private readonly log4net.ILog _log;

		private readonly GraphicsDeviceManager _graphics;
		private readonly ContentManager _content;
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;

		private bool _debugToggling;
		private bool _debugEnabled;

		/// <summary>
		/// Initialize a new instance of <c>DebugDisplay</c>.
		/// </summary>
		/// <param name="game">The game instance.</param>
		/// <param name="graphics">The graphics device manager.</param>
		internal DebugDisplay(Game game, GraphicsDeviceManager graphics) : base(game)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("DebugDisplay created!");
			_graphics = graphics;
			_content = new ContentManager(game.Services, Constants.ContentDirectory);
			_log.Debug("Creating FrameRateDisplay...");
			game.Components.Add(new FrameRateDisplay(game));
			game.Exiting += (s, e) => UnloadContent();
		}

		/// <summary>
		/// Loads all content used by <see cref="DebugDisplay" />.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = _content.Load<SpriteFont>(Constants.DebugFont);
		}

		/// <summary>
		/// Unloads content loaded by <see cref="LoadContent" />.
		/// </summary>
		protected override void UnloadContent()
		{
			_log.Debug("DebugDisplay is unloading!");
			_content.Unload();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.F3) && !_debugToggling)
			{
				_debugToggling = true;
				_debugEnabled = !_debugEnabled;
				_log.Debug("Debug display is now " + (_debugEnabled ? "ENABLED" : "DISABLED"));
			}

			if (Keyboard.GetState().IsKeyUp(Keys.F3))
				_debugToggling = false;
		}

		/// <summary>
		/// Draws the debug information.
		/// </summary>
		/// <param name="gameTime">N/A (Not Used) (See XNA Documentation)</param>
		public override void Draw(GameTime gameTime)
		{
			if (!_debugEnabled)
				return;

			_spriteBatch.Begin();
			_spriteBatch.DrawString(_font, "M_X: " + Mouse.GetState().X, new Vector2(32, 80), Color.Black);
			_spriteBatch.DrawString(_font, "M_Y: " + Mouse.GetState().Y, new Vector2(32, 96), Color.Black);
			if (_graphics.IsFullScreen)
				_spriteBatch.DrawString(_font, "FULLSCREEN", new Vector2(32, 152), Color.Red);
			_spriteBatch.End();
		}
	}
}
