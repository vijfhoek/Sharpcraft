using System;
using Sharpcraft.Networking.Packets;

namespace Sharpcraft.Networking
{
	/// <summary>
	/// The <c>LoginEventArgs</c> class used for the login event.
	/// </summary>
	public class LoginEventArgs : EventArgs
	{
		/// <summary>
		/// The <see cref="LoginResult" /> of the login.
		/// </summary>
		public LoginResult Result;

		/// <summary>
		/// Initializes a new instance of the <c>LoginEventArgs</c> class.
		/// </summary>
		/// <param name="result"></param>
		internal LoginEventArgs(LoginResult result)
		{
			Result = result;
		}
	}

	/// <summary>
	/// The <c>PacketEventArgs</c> class used for the packet (received) event.
	/// </summary>
	public class PacketEventArgs : EventArgs
	{
		public Packet Packet { get; private set; }

		internal PacketEventArgs(Packet packet)
		{
			Packet = packet;
		}
	}

	/// <summary>
	/// The login event handler.
	/// </summary>
	/// <param name="e">The <see cref="LoginEventArgs" /> associated with this event.</param>
	public delegate void LoginEventHandler(LoginEventArgs e);

	/// <summary>
	/// The packet event handler.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="eventArgs">The <see cref="Packet" /> that was received.</param>
	public delegate void PacketEventHandler(object sender, PacketEventArgs eventArgs);
}
