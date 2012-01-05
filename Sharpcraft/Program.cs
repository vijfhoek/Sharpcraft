/* 
 * Sharpcraft
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

#define DEVELOPMENT

using System;
using System.IO;
using log4net;

using Sharpcraft.Logging;

namespace Sharpcraft
{
#if WINDOWS || XBOX
	static class Program
	{
		private static readonly ILog Log = LoggerManager.GetLogger(typeof(Program));

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			Log.Info("!!! APPLICATION LOAD !!!");
			Log.Info("Sharpcraft is loading...");
			try
			{
				Log.Debug("Creating protocol...");
				var protocol = new Protocol.Protocol("localhost", 25565);

				Log.Debug("Sending handshake packet.");
				protocol.PacketHandshake("Sharpcraft");
				protocol.GetPacket();
				Log.Debug("Sending login request.");
				protocol.PacketLoginRequest(22, "Sharpcraft");
				protocol.GetPacket();

				using (var game = new Sharpcraft())
				{
					Log.Debug("Running game (Game.Run()).");
					game.Run();
				}
				Log.Info("!!! APPLICATION EXIT !!!");
			}
			catch(FileNotFoundException ex)
			{
				Log.Fatal("Required file \"" + ex.FileName + "\" not found! Application is exiting...");
				Environment.Exit(1);
			}
			catch(System.Net.Sockets.SocketException ex)
			{
				Log.Error("Failed to connect to target server, " + ex.GetType() + " was thrown.");
				Log.Error(ex.GetType() + ": " + ex.Message);
				Log.Error("Stack Trace:\n" + ex.StackTrace);
				Log.Error("Exiting...");
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

