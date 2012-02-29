/*
 * LogManager.cs
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

using log4net;
using log4net.Config;

namespace Sharpcraft.Logging
{
	/// <summary>
	/// Provides centralized logging utilities for Sharpcraft.
	/// </summary>
	/// <remarks>Using the default config files,
	/// the log files will be contained withing the "log" subfolder.
	/// The format of the log file names will be "Sharpcraft_yyyy-mm-dd.log".</remarks>
	public static class LogManager
	{
		/// <summary>
		/// <c>bool</c> indicating whether or not the configuration file has been loaded.
		/// </summary>
		private static bool _loaded;
		/// <summary>
		/// Relative ath to the config file.
		/// </summary>
		private const string ConfigFile = @"configs\LoggerConfig.xml";
		/// <summary>
		/// Relative path to the debug config file.
		/// </summary>
		private const string DebugConfigFile = @"configs\LoggerDebugConfig.xml";

		/// <summary>
		/// Load the logger config.
		/// </summary>
		/// <param name="debug"><c>true</c> to load debug config, <c>false</c> otherwise.
		/// Defaults to <c>false</c>.</param>
		public static void LoadConfig(bool debug = false)
		{
			XmlConfigurator.Configure(new FileInfo(debug ? DebugConfigFile : ConfigFile));
			_loaded = true;
		}

		/// <summary>
		/// Get an <see cref="ILog" /> to utilize logging.
		/// </summary>
		/// <param name="sender">Object or Type reference for the object requiring logger.</param>
		/// <returns>The <see cref="ILog" /> for use with logging.</returns>
		/// <remarks>By using this method to get a logger instead of <c>LogManager.GetLogger</c>,
		/// you avoid the need to call <c>XmlConfigurator.Configure</c> in every class you want to log from.</remarks>
		/// <example>For a class:
		/// <code>ILog log = Sharpcraft.Logging.LoggerManager.GetLogger(this);</code>
		/// For a static class:
		/// <code>ILog log = Sharpcraft.Logging.LoggerManager.GetLogger(typeof(MyStaticClass));</code></example>
		public static ILog GetLogger(object sender)
		{
			if (!_loaded)
				LoadConfig();
			return log4net.LogManager.GetLogger(sender.GetType().ToString() == "System.RuntimeType" ? (Type) sender : sender.GetType());
		}
	}
}
