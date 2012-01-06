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
		private const string ExceptionFile = @"logs\exception.log";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			bool cleanExit = true;

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
				if (string.IsNullOrEmpty(ext))
					continue;
				ext = ext.Substring(1);
				if (ext == "dll" || ext == "exe")
				{
					string version = AssemblyName.GetAssemblyName(file).Version.ToString();
					string name = Path.GetFileNameWithoutExtension(file);
					_log.Info(name + " v" + version);
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
			}
			catch(FileNotFoundException ex)
			{
				_log.Fatal("Required file \"" + ex.FileName + "\" not found! Application is exiting...");
				cleanExit = false;
			}
			catch(System.Net.Sockets.SocketException ex)
			{
				_log.Error("Failed to connect to target server, " + ex.GetType() + " was thrown.");
				_log.Error(ex.GetType() + ": " + ex.Message);
				_log.Error("Stack Trace:\n" + ex.StackTrace);
				_log.Error("Exiting...");
				cleanExit = false;
			}
			catch(Exception ex)
			{
				_log.Fatal("Unknown exception " + ex.GetType() + " thrown. Writing exception info to logs\\exception.log");
				WriteExceptionToFile(ex);
				cleanExit = false;
#if DEVELOPMENT
				throw;
#endif
			}
			finally
			{
				_log.Info("Clean Exit: " + (cleanExit ? "TRUE" : "FALSE"));
				_log.Info("!!! APPLICATION EXIT !!!"); 
			}
		}

		private static void WriteExceptionToFile(Exception ex)
		{
			try
			{
				var writer = new StreamWriter(ExceptionFile, false);
				string date = DateTime.Now.ToString();
				writer.WriteLine("Fatal exception occurred at " + date);
				writer.WriteLine("The exception thrown was " + ex.GetType());
				writer.WriteLine("ToString() => " + ex);
				writer.WriteLine("Exception message: " + ex.Message);
				writer.WriteLine("Stack Trace:\n" + ex.StackTrace);
				writer.WriteLine("\nDone writing exception info.");
				writer.Flush();
				writer.Close();
			}
			catch(IOException)
			{
				_log.Error("Unable to write exception info to file.");
			}
		}
	}
#endif
}

