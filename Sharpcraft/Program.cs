/* 
 * Sharpcraft
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

#define DEVELOPMENT

using System;
using System.IO;
using System.Reflection;

using log4net;

using Sharpcraft.Logging;

namespace Sharpcraft
{
#if WINDOWS || XBOX
	static class Program
	{
		private static ILog _log;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
#if DEVELOPMENT
			bool debug = true;
#else
			bool debug = false;
#endif
			if (args.Length > 0)
				if (args[0].ToLower() == "debug")
					debug = true;
			LoggerManager.LoadConfig(debug);
			_log = LoggerManager.GetLogger(typeof (Program));
			_log.Info("!!! APPLICATION LOAD !!!");
			_log.Info("Detecting components...");
			foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory()))
			{
				string ext = Path.GetExtension(file);
				if (!string.IsNullOrEmpty(ext))
				{
					ext = ext.Substring(1);
					if (ext == "dll" || ext == "exe")
					{
						string version = AssemblyName.GetAssemblyName(file).Version.ToString();
						string name = Path.GetFileNameWithoutExtension(file);
						_log.Info(name + " v" + version);
					}
				}
			}
			_log.Info("Components detected!");
			_log.Info("Sharpcraft is loading...");
			try
			{
				_log.Debug("Creating protocol...");
				var protocol = new Networking.Protocol("localhost", 25565);

				_log.Debug("Sending handshake packet.");
				protocol.PacketHandshake("Sharpcraft");
				protocol.GetPacket();
				_log.Debug("Sending login request.");
				protocol.PacketLoginRequest(22, "Sharpcraft");
				protocol.GetPacket();

				using (var game = new Sharpcraft())
				{
					_log.Debug("Running game (Game.Run()).");
					game.Run();
				}
				_log.Info("!!! APPLICATION EXIT !!!");
			}
			catch(FileNotFoundException ex)
			{
				_log.Fatal("Required file \"" + ex.FileName + "\" not found! Application is exiting...");
				Environment.Exit(1);
			}
			catch(System.Net.Sockets.SocketException ex)
			{
				_log.Error("Failed to connect to target server, " + ex.GetType() + " was thrown.");
				_log.Error(ex.GetType() + ": " + ex.Message);
				_log.Error("Stack Trace:\n" + ex.StackTrace);
				_log.Error("Exiting...");
				Environment.Exit(1);
			}
#if !DEVELOPMENT
			catch(Exception ex)
			{
				Log.Fatal("Unknown exception " + ex.GetType() + " thrown. Details below:");
				Log.Fatal("Exception " + ex.GetType());
				Log.Fatal("Message: " + ex.Message);
				Log.Fatal("Stack Trace:\n" + ex.StackTrace);
				Log.Fatal("Cannot continue application execution, exiting...");
				Environment.Exit(1);
			}
#endif
		}
	}
#endif
}

