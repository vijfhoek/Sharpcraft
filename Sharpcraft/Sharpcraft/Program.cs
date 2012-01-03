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
            var prot = new Protocol("217.123.107.137", 25565);
            prot.PacketHandshake("vijfhoek2");

            using (var game = new Sharpcraft())
            {
                game.Run();
            } 
        }
    }
#endif
}

