/* 
 * Sharpcraft
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

using log4net;

//using Sharpcraft.Forms;
using Sharpcraft.Logging;

namespace Sharpcraft
{
#if WINDOWS || XBOX
	/// <summary>
	/// Main class for Sharpcraft, all initial loading is done here.
	/// </summary>
	static class Program
	{
		/// <summary>
		/// Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
		/// </summary>
		/// <param name="nStdHandle">The standard device.</param>
		/// <returns>If the function succeeds, the return value is a handle to the specified device, or a redirected handle set by a previous call to SetStdHandle.
		/// If the function fails, the return value is INVALID_HANDLE_VALUE.</returns>
		/// <remarks>Documentation from MSDN.</remarks>
		[DllImport("kernel32", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		/// <summary>
		/// Allocates a new console for the calling process.
		/// </summary>
		/// <returns><c>true</c> if function succeeds, <c>false</c> otherwise.</returns>
		/// <remarks>Documentation from MSDN.</remarks>
		[DllImport("kernel32", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall)]
		private static extern bool AllocConsole();

		/// <summary>
		/// Detaches the calling process from its console.
		/// </summary>
		/// <returns>If the function succeeds, the return value is nonzero.
		/// If the function fails, the return value is zero.</returns>
		/// <remarks>Documentation from MSDN.</remarks>
		[DllImport("kernel32", EntryPoint = "FreeConsole", SetLastError = true, CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall)]
		private static extern int FreeConsole();

		/// <summary>
		/// The standard output device.
		/// </summary>
		private const int STD_OUTPUT_HANDLE = -11;
		/// <summary>
		/// Code page to use for console.
		/// </summary>
		private const int CODE_PAGE = 437;

		/// <summary>
		/// Log object for this class.
		/// </summary>
		private static ILog _log;
		/// <summary>
		/// File to write all unhandled exceptions to.
		/// </summary>
		private const string ExceptionFile = @"logs\exception.log";

		private static Launcher _launcher;
		private static Sharpcraft _game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">Command Line Arguments.</param>
		static void Main(string[] args)
		{
			bool cleanExit = true;

#if DEBUG
			bool debug = true;
#else
			bool debug = false;
#endif
			if (args.Length > 0)
				if (args[0].ToLower() == "debug")
					debug = true;

			if (debug && !System.Diagnostics.Debugger.IsAttached)
			{
				AllocConsole();
				IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
				var safeFileHandle = new SafeFileHandle(stdHandle, true);
				var fileStream = new FileStream(safeFileHandle, FileAccess.Write);
				var encoding = System.Text.Encoding.GetEncoding(CODE_PAGE);
				var stdOut = new StreamWriter(fileStream, encoding) {AutoFlush = true};
				Console.SetOut(stdOut);
			}
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
				//var protocol = new Networking.Protocol("localhost", 25565);

				_log.Debug("Sending handshake packet.");
				//protocol.PacketHandshake("Sharpcraft");
				//protocol.GetPacket();
				_log.Debug("Sending login request.");
				//protocol.PacketLoginRequest(22, "Sharpcraft");
				//protocol.GetPacket();

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				_log.Debug("Running game (Game.Run()).");
				_game = new Sharpcraft();
				_launcher = new Launcher();
				Application.Run(_launcher);
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
#if DEBUG
				throw;
#endif
			}
			finally
			{
				_log.Info("Clean Exit: " + (cleanExit ? "TRUE" : "FALSE"));
				_log.Info("!!! APPLICATION EXIT !!!");
				FreeConsole();
			}
		}

		/// <summary>
		/// Writes exception info to file.
		/// </summary>
		/// <param name="ex">The exception to write.</param>
		/// <remarks>This is generally only used for unhandled/fatal exceptions
		/// that will cause the application to exit.</remarks>
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
