using System;
using System.IO;

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
				var prot = new Protocol("localhost", 25565);
				
				prot.PacketHandshake("Sharpcraft");
				Packet packet = prot.GetPacket();

				prot.PacketLoginRequest(22, "Sharpcraft");
				packet = prot.GetPacket();

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
			catch(Exception ex)
			{
				Console.WriteLine("Unexpected exception " + ex.GetType());
			}
		}
	}
#endif
}

