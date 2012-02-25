using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Sharpcraft.Logging;

namespace Sharpcraft.Components.Debug
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class UserInfoDisplay : DrawableGameComponent
	{
		private log4net.ILog _log;

		private readonly ContentManager _content;
		private SpriteBatch _spriteBatch;
		private SpriteFont _font;

		private bool _userInfoToggling;
		private bool _userInfoEnabled;

		private Sharpcraft sc;

		internal UserInfoDisplay(Game game) : base(game)
		{
			_log = LogManager.GetLogger(this);
			_log.Debug("UserInfoDisplay created!");
			sc = (Sharpcraft) game;
			_content = new ContentManager(game.Services, Constants.ContentDirectory);
			game.Exiting += (s, e) => UnloadContent();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = _content.Load<SpriteFont>(Constants.DebugFont);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			_log.Info("UserInfoDisplay is unloading!");
			_content.Unload();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.F3) && !_userInfoToggling)
			{
				_userInfoToggling = true;
				_userInfoEnabled = !_userInfoEnabled;
			}

			if (Keyboard.GetState().IsKeyUp(Keys.F3))
				_userInfoToggling = false;
		}

		public override void Draw(GameTime gameTime)
		{
			if (!_userInfoEnabled)
				return;

			_spriteBatch.Begin();
			if (sc.Client.GetPlayer() != null)
			{
				_spriteBatch.DrawString(_font, "P_X: " + sc.Client.GetPlayer().Position.X, new Vector2(32, 112), Color.Black);
				_spriteBatch.DrawString(_font, "P_Y: " + sc.Client.GetPlayer().Position.Y, new Vector2(32, 128), Color.Black);
				_spriteBatch.DrawString(_font, "P_Z: " + sc.Client.GetPlayer().Position.Z, new Vector2(32, 144), Color.Black);
				_spriteBatch.DrawString(_font, "PDY: " + sc.Client.GetPlayer().Direction.Yaw, new Vector2(32, 160), Color.Black);
				_spriteBatch.DrawString(_font, "PDP: " + sc.Client.GetPlayer().Direction.Pitch, new Vector2(32, 176), Color.Black);
			}
			_spriteBatch.End();
		}
	}
}
