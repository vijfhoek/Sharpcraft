using System;

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
	/// The login event handler.
	/// </summary>
	/// <param name="e">The <see cref="LoginEventArgs" /> associated with this event.</param>
	public delegate void LoginEventHandler(LoginEventArgs e);
}
