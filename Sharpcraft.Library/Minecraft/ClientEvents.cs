using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharpcraft.Library.Minecraft
{
	/// <summary>
	/// Event args for Chat Message Received event.
	/// </summary>
	public class ChatMessageReceivedEventArgs : EventArgs
	{
		/// <summary>
		/// The chat message received.
		/// </summary>
		public readonly string Message;

		internal ChatMessageReceivedEventArgs(string message)
		{
			Message = message;
		}
	}

	/// <summary>
	/// Event handler for Chat Message Received.
	/// </summary>
	/// <param name="e">The <see cref="ChatMessageReceivedEventArgs" /> object.</param>
	public delegate void ChatMessageReceivedEventHandler(ChatMessageReceivedEventArgs e);
}
