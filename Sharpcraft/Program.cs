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
			var prot = new Protocol("localhost", 25565);
			prot.PacketHandshake("Sharpcraft");

			using (var game = new Sharpcraft())
			{
				game.Run();
			} 
		}
	}
#endif
}

