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
            Protocol prot = new Protocol("217.123.107.137", 25565);
            prot.Packet2Handshake("vijfhoek2");

            using (Sharpcraft game = new Sharpcraft())
            {
                game.Run();
            } 
        }
    }
#endif
}

