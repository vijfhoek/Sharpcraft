namespace Sharpcraft
{
	/// <summary>
	/// Represents the currently logged in minecraft user.
	/// </summary>
	public class User
	{
		/// <summary>
		/// The session ID returned by the minecraft servers at login.
		/// </summary>
		public string SessionID { get; private set; }

		/// <summary>
		/// The name of this user.
		/// </summary>
		private string _name;

		/// <summary>
		/// Initialize a new instance of the <c>User</c> class.
		/// </summary>
		/// <param name="name">Name of the user.</param>
		/// <param name="sessionId">Session ID for this user.</param>
		/// <remarks>All of the parameters are optional and can be set later.</remarks>
		internal User(string name = null, string sessionId = null)
		{
			_name = name;
			SessionID = sessionId;
		}

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <param name="pretty"><c>true</c> to capitalize the first letter and make the rest lowercase, <c>false</c> for unmodified.</param>
		/// <returns>The user's name.</returns>
		public string GetName(bool pretty = false)
		{
			if (pretty)
			{
				char[] a = _name.ToCharArray();
				a[0] = char.ToUpper(a[0]);
				return new string(a);
			}
			return _name;
		}

		/// <summary>
		/// Set the name of the user.
		/// </summary>
		/// <param name="name">The new name to set.</param>
		/// <remarks>Note that this is entirely client-side,
		/// this does not update the actual username on the minecraft servers.</remarks>
		public void SetName(string name)
		{
			_name = name;
		}

		/// <summary>
		/// Set this user's session ID.
		/// </summary>
		/// <param name="id">The session ID to set.</param>
		public void SetSessionID(string id)
		{
			SessionID = id;
		}
	}
}
