/* 
 * Sharpcraft.Logging
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
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
