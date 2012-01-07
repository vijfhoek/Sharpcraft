/* 
 * Sharpcraft
 * Copyright (c) 2012 by Sijmen Schoon and Adam Hellberg.
 * All Rights Reserved.
 */

//#define SC_DIRECT

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

//using Sharpcraft.Forms;
using Sharpcraft.Logging;
using Sharpcraft.Protocol;

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
		/// The application context.
		/// </summary>
		private static ApplicationContext _context;

		/// <summary>
		/// Log object for this class.
		/// </summary>
		private static log4net.ILog _log;
		/// <summary>
		/// File to write all unhandled exceptions to.
		/// </summary>
		private const string ExceptionFile = @"logs\exception.log";

		/// <summary>
		/// The game launcher.
		/// </summary>
		private static Launcher _launcher;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">Command Line Arguments.</param>
		static void Main(string[] args)
		{
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
			LogManager.LoadConfig(debug);
			_log = LogManager.GetLogger(typeof (Program));
			_log.Info("!!! APPLICATION LOAD !!!");
			_log.Info("Detecting components...");
			foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory()))
			{
<<<<<<< HEAD
				_log.Debug("Creating protocol...");
				var protocol = new Protocol.Protocol("localhost", 25565);

				_log.Debug("Sending handshake packet.");
				var packetHandshakeCS = new PacketHandshakeCS();
				packetHandshakeCS.Username = "Sharpcraft";
				protocol.SendPacket(packetHandshakeCS);

				protocol.GetPacket();

				_log.Debug("Sending login request.");
				var packetLoginRequestCS = new PacketLoginRequestCS();
				packetLoginRequestCS.ProtocolVersion = 22;
				packetLoginRequestCS.Username = "Sharpcraft";

				protocol.GetPacket();

				using (var game = new Sharpcraft())
=======
				string ext = Path.GetExtension(file);
				if (string.IsNullOrEmpty(ext))
					continue;
				ext = ext.Substring(1);
				if (ext == "dll" || ext == "exe")
>>>>>>> 0931227a66c4afb8254f3d61cf45ceff65f523c2
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
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
#if SC_DIRECT
				new Sharpcraft().Run();
#else
				_log.Debug("Starting launcher...");
				_launcher = new Launcher();
				_launcher.Show();
				_context = new ApplicationContext(_launcher);
				Application.Run(_context);
				_log.Info("Launcher has returned execution to main thread.");
#endif
			}
			catch(FileNotFoundException ex)
			{
				_log.Fatal("Required file \"" + ex.FileName + "\" not found! Application is exiting...");
			}
			catch(Exception ex)
			{
				_log.Fatal("Unknown exception " + ex.GetType() + " thrown. Writing exception info to logs\\exception.log");
				WriteExceptionToFile(ex);
#if DEBUG
				throw;
#endif
			}
			finally
			{
				if (_launcher != null)
				{
					_log.Debug("Closing launcher.");
					_launcher.Close();
					_launcher.Dispose();
					_launcher = null;
					_log.Info("Launcher closed.");
				}
				if (debug)
				{
					_log.Debug("Closing debug console.");
					FreeConsole();
				}
				_log.Info("!!! APPLICATION EXIT !!!");
			}
		}

		/// <summary>
		/// Quits the application.
		/// </summary>
		public static void Quit()
		{
			_context.ExitThread();
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
