/*
 * Sharpcraft.cs
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
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using System;
using System.IO;
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

using Newtonsoft.Json;

using Sharpcraft.Steam;
using Sharpcraft.Forms;
using Sharpcraft.Logging;
using Sharpcraft.Networking;
using Sharpcraft.Components.Debug;
using Sharpcraft.Library.GUI;
using Sharpcraft.Library.Minecraft;
using Sharpcraft.Library.Minecraft.Entities;
using Sharpcraft.Library.Configuration;

using Label = Sharpcraft.Library.GUI.Label;

namespace Sharpcraft
{
	/// <summary>
	/// Main class of Sharpcraft, this is the game itself.
	/// </summary>
	/// <remarks>Most documentation in this class comes from XNA.</remarks>
	public class Sharpcraft : Game
	{
		/// <summary>
		/// Log object for this class.
		/// </summary>
		private readonly log4net.ILog _log;

		/// <summary>
		/// Settings specific for the game.
		/// </summary>
		private readonly GameSettings _settings;

		/// <summary>
		/// The graphics device manager.
		/// </summary>
		private GraphicsDeviceManager _graphics;

		/// <summary>
		/// Sprite batch.
		/// </summary>
		private SpriteBatch _spriteBatch;

		/// <summary>
		/// Whether or not the game menu should be rendered.
		/// </summary>
		private bool _gameMenuOpen;

		/// <summary>
		/// Whether or not the user is currently toggling the game menu.
		/// </summary>
		private bool _menuToggling;

		/// <summary>
		/// Whether or not fullscreen mode is currently toggling.
		/// </summary>
		private bool _fullscreenToggling;

		/// <summary>
		/// Whether or not the player is currently in a game server.
		/// </summary>
		private bool _inServer = true;

		/// <summary>
		/// The crosshair [DEBUG].
		/// </summary>
		private Texture2D _crosshair;

		/// <summary>
		/// Font used for the (pause) menu.
		/// </summary>
		private SpriteFont _menuFont;

		/// <summary>
		/// The label used for the in-game pause menu.
		/// </summary>
		private Label _menuLabel;

		/// <summary>
		/// The user.
		/// </summary>
		private User _user;

		private Server _server;
		internal Client Client { get; private set; }

		// Testing
		private System.Windows.Forms.Label _testLabel;

		/// <summary>
		/// Initializes a new instance of Sharpcraft.
		/// </summary>
		public Sharpcraft(User user)
		{
			_log = LogManager.GetLogger(this);
			_settings = new GameSettings(Constants.GameSettings);
			_user = user;
			if (File.Exists(Constants.GameSettings))
			{
				_log.Info("Loading game settings from file...");
				var reader = new StreamReader(Constants.GameSettings);
				_settings = new JsonSerializer().Deserialize<GameSettings>(new JsonTextReader(reader));
				_log.Info("Game settings loaded successfully!");
				reader.Close();
			}
			else
			{
				_settings = new GameSettings(Constants.GameSettings) {Size = new Point(1280, 720)};
			}
			_log.Debug("Initializing graphics device.");
			_graphics = new GraphicsDeviceManager(this);
			if (_settings.Fullscreen)
			{
				_graphics.IsFullScreen = true;
				_settings.Size = new Point(SystemInformation.PrimaryMonitorSize.Width, SystemInformation.PrimaryMonitorSize.Height);
			}
			_graphics.PreferredBackBufferWidth = _settings.Size.X;
			_graphics.PreferredBackBufferHeight = _settings.Size.Y;
			_log.Debug("Setting content directory.");
			Content.RootDirectory = Constants.ContentDirectory;
			_log.Debug("Creating DebugDisplay...");
			Components.Add(new DebugDisplay(this, _graphics));
#if DEBUG
			_gameMenuOpen = true;
#endif
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
			_log.Info("Sharpcraft is initializing!");

			base.Initialize();

			/* /!\ Steam hardcore loading action /!\ */
			_log.Info("Loading Steam components...");
			if (SteamManager.Init())
			{
				//SteamManager.FriendList.LoadFriends(); // Should load automatically now
				//Application.EnableVisualStyles();
				_log.Info("Creating Steam GUI.");
				// TODO: Find a way to set the start location of SteamGUI to be next to Game Window.
				var steamGUI = new SteamGUI();
				if (!steamGUI.Visible)
					steamGUI.Show();
				_log.Info("Steam components loaded!");
			}
			else
			{
				_log.Info("Steam not installed or not running, Steam functionality will NOT be available.");
			}

			// /!\ WARNING /!\
			// Ugly debug code ahead!
			_log.Debug("Starting debug connection...");
			_server = new Server("F16Gaming Test", "localhost", 25565, "The test server", 0, 0, 0, true);
			Client = new Client(_server, new Player(0, "Sharpcraft"));
			Client.Connect();
			_log.Debug("Reached end of debug connection!");

			Exiting += (s, e) => Client.Exit();

			_testLabel = new System.Windows.Forms.Label();
			_testLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			_testLabel.AutoSize = true;
			_testLabel.Location = new System.Drawing.Point(10, 10);
			_testLabel.Name = "_testLabel";
			_testLabel.Text = "Hello, World!";
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			_log.Debug("LoadContent();");
			_log.Info("!!! GAME LOAD !!!");
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_crosshair = Content.Load<Texture2D>("crosshair");
			_menuFont = Content.Load<SpriteFont>(Constants.MenuFont);
			_menuLabel = new Label("!!! GAME MENU OPEN !!!", _menuFont, Color.Yellow);
			_log.Debug("LoadContent(); ## END ##");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			_log.Info("!!! GAME UNLOAD !!!");
			_log.Debug("UnloadContent();");
			SteamManager.Close();
			
			_log.Debug("UnloadContent(); ## END ##");
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				ToggleGameMenu();

			if (Keyboard.GetState().IsKeyUp(Keys.Escape))
				_menuToggling = false;

			if (!_gameMenuOpen && IsActive)
				Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

			if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Enter))
				ToggleFullscreen();
			else
				_fullscreenToggling = false;
			

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_spriteBatch.Begin();
			_spriteBatch.Draw(_crosshair, new Vector2(Mouse.GetState().X - 24, Mouse.GetState().Y - 24), Color.White);
			if (_gameMenuOpen)
			{
				//float tWidth = _menuFont.MeasureString("!!! GAME MENU OPEN !!!").X;
				//_spriteBatch.DrawString(_menuFont, "!!! GAME MENU OPEN !!!", new Vector2((float) GraphicsDevice.Viewport.Width / 2 - tWidth / 2, (float) GraphicsDevice.Viewport.Height / 2 + _menuFont.LineSpacing), Color.Yellow);
				var menuPos =
					_menuLabel.GetCenterPosition(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
					                             new Vector2(0, _menuLabel.Height + 5));
				_spriteBatch.DrawString(_menuLabel.Font, _menuLabel.Text, menuPos, _menuLabel.ForeColor);
			}
			_spriteBatch.End();
			
			base.Draw(gameTime);
		}

		/// <summary>
		/// Event handler for when the game loses focus.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See XNA Documentation)</param>
		/// <param name="args">N/A (Not Used) (See XNA Documentation)</param>
		/// <remarks>Displays game menu to allow mouse movement when game is minimized/in the background.</remarks>
		protected override void OnDeactivated(object sender, EventArgs args)
		{
			_gameMenuOpen = true;
			base.OnDeactivated(sender, args);
		}

		private void ToggleGameMenu()
		{
			if (!_inServer || _menuToggling)
				return;
			_menuToggling = true;
			_gameMenuOpen = !_gameMenuOpen;
			_log.Debug("Game menu is now " + (_gameMenuOpen ? "open" : "closed"));
		}

		private void ToggleFullscreen()
		{
			if (_fullscreenToggling)
				return;
			_fullscreenToggling = true;
			_settings.Fullscreen = !_settings.Fullscreen;
			_graphics.IsFullScreen = _settings.Fullscreen;
			_graphics.ApplyChanges();
			_log.Debug("Fullscreen is now " + (_settings.Fullscreen ? "enabled" : "disabled"));
		}

		/// <summary>
		/// Writes settings to file and then exits the game.
		/// </summary>
		/// <param name="sender">N/A (Not Used) (See XNA Documentation)</param>
		/// <param name="args">N/A (Not Used) (See XNA Documentation)</param>
		protected override void OnExiting(object sender, EventArgs args)
		{
			_log.Info("!!! GAME EXIT !!!");
			_settings.WriteToFile();
			base.OnExiting(sender, args);
		}
	}
}
