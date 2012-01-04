/* 
 * Sharpcraft
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

//#define DEVELOPMENT

using System;
using System.IO;

using log4net;

using Sharpcraft.Logging;

namespace Sharpcraft
{
#if WINDOWS || XBOX
	static class Program
	{
		private static ILog _log = LoggerManager.GetLogger(typeof(Program));

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			_log.Info("!!! APPLICATION LOAD !!!");
			_log.Info("Sharpcraft is loading...");
			try
			{
				_log.Debug("Creating protocol...");
				var protocol = new Protocol.Protocol("localhost", 25565);

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
#if !DEVELOPMENT
			catch(Exception ex)
			{
				_log.Fatal("Unknown exception " + ex.GetType() + " thrown. Details below:");
				_log.Fatal("Exception " + ex.GetType());
				_log.Fatal("Message: " + ex.Message);
				_log.Fatal("Stack Trace:\n" + ex.StackTrace);
				_log.Fatal("Cannot continue application execution, exiting...");
				Environment.Exit(1);
			}
#endif
		}
	}
#endif
}

