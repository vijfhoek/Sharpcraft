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
		private log4net.ILog _log;

		private ContentManager _content;
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;

		private bool _debugToggling;
		private bool _debugEnabled;

		/// <summary>
		/// Initialize a new instance of <c>DebugDisplay</c>.
		/// </summary>
		/// <param name="game">The game instance.</param>
		internal DebugDisplay(Game game) : base(game)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("DebugDisplay created!");
			_content = new ContentManager(game.Services, SharpcraftConstants.ContentDirectory);
			_log.Debug("Creating FrameRateDisplay...");
			game.Components.Add(new FrameRateDisplay(game));
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = _content.Load<SpriteFont>(SharpcraftConstants.DebugFont);
		}

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
			}

			if (Keyboard.GetState().IsKeyUp(Keys.F3))
				_debugToggling = false;
		}

		public override void Draw(GameTime gameTime)
		{
			if (!_debugEnabled)
				return;

			_spriteBatch.Begin();
			_spriteBatch.DrawString(_font, "M_X: " + Mouse.GetState().X, new Vector2(32, 80), Color.Black);
			_spriteBatch.DrawString(_font, "M_Y: " + Mouse.GetState().Y, new Vector2(32, 96), Color.Black);
			_spriteBatch.End();
		}
	}
}
