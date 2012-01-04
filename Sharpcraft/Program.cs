#define DEBUG

using System;
using System.IO;
using Sharpcraft.Protocol;

namespace Sharpcraft
{
#if WINDOWS || XBOX
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			try
			{
				Protocol.Protocol prot = new Protocol.Protocol("localhost", 25565);

				prot.PacketHandshake("Sharpcraft");
				prot.GetPacket();
				prot.PacketLoginRequest(22, "Sharpcraft");
				prot.GetPacket();

				using (var game = new Sharpcraft())
				{
					game.Run();
				}
			}
			catch(FileNotFoundException ex)
			{
				Console.WriteLine("Required file {0} was not found!", ex.FileName);
				Console.WriteLine(ex.Message);
			}
#if !DEBUG
			catch(Exception ex)
			{
				Console.WriteLine("Unexpected exception " + ex.GetType());
			}
#endif
		}
	}
#endif
}

