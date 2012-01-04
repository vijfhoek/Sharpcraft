/* 
 * Sharpcraft
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

using Keys = Microsoft.Xna.Framework.Input.Keys;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

using log4net;

using Sharpcraft.Steam;
using Sharpcraft.Logging;

namespace Sharpcraft
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Sharpcraft : Game
	{
		private readonly ILog _log;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public Sharpcraft()
		{
			_log = LoggerManager.GetLogger(this);
			_log.Debug("Initializing graphics device.");
			_graphics = new GraphicsDeviceManager(this);
			_log.Debug("Setting content directory.");
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			_log.Debug("Initialize();");
			// TODO: Add your initialization logic here

			base.Initialize();

			/* /!\ Steam hardcore loading action /!\ */
			_log.Info("Loading Steam components...");
			if (SteamManager.Init())
			{
				//SteamManager.FriendList.LoadFriends(); // Should load automatically now
				Application.EnableVisualStyles();
				_log.Info("Creating Steam GUI.");
				var steamGUI = new SteamGUI.SteamGUI();
				steamGUI.Location = new System.Drawing.Point(Window.ClientBounds.Right, Window.ClientBounds.Top);
				if (!steamGUI.Visible)
					steamGUI.Show();
				_log.Info("Steam components loaded!");
			}
			else
			{
				_log.Info("Steam not installed or not running, Steam functionality will NOT be available.");
			}
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			_log.Debug("LoadContent();");
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			_log.Info("!!! APPLICATION UNLOAD !!!");
			_log.Debug("UnloadContent();");
			// TODO: Unload any non ContentManager content here
			SteamManager.Close();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				Exit();
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
