using System;
using System.IO;
using System.Text;
using System.Linq;

using Newtonsoft.Json;

namespace Sharpcraft.Library.Configuration
{
	/// <summary>
	/// Used to store settings for the Sharpcraft launcher.
	/// </summary>
	public class LauncherSettings : Settings
	{
		/// <summary>
		/// The key to use when encrypting and decrypting.
		/// </summary>
		[JsonIgnore]
		private const string Key = "~L}%JiYK3&Vq]ezC";

		/// <summary>
		/// The username of the most recently logged in user.
		/// </summary>
		public string Username;

		/// <summary>
		/// The user's password.
		/// </summary>
		[JsonProperty("Password")]
		private string _password;

		/// <summary>
		/// Whether or not to remember the password.
		/// </summary>
		public bool Remember;

		/// <summary>
		/// Initialize a new instance of the <c>LauncherSettings</c> class.
		/// </summary>
		/// <param name="settingsFile">The file containing a JSON serialization of this object.</param>
		public LauncherSettings(string settingsFile) : base(settingsFile) { }

		/// <summary>
		/// Set the user's password.
		/// </summary>
		/// <param name="password">Plaintext version of password.</param>
		/// <remarks>The password will be encrypted using XOR and then converted into a Base64 string.</remarks>
		public void SetPassword(string password)
		{
			var pass = Encoding.UTF8.GetBytes(password);
			var data = pass.Select((c, i) => (byte) (c ^ (i % Key.Length))).ToArray();
			_password = Convert.ToBase64String(data);
		}

		/// <summary>
		/// Get plaintext version of the encrypted password.
		/// </summary>
		/// <returns>Plaintext version of password.</returns>
		public string GetPassword()
		{
			if (string.IsNullOrEmpty(_password))
				return null;
			var pass = Convert.FromBase64String(_password);
			return Encoding.UTF8.GetString(pass.Select((c, i) => (byte) (c ^ (i % Key.Length))).ToArray());
		}

		public override void WriteToFile()
		{
			try
			{
				base.WriteToFile();
			}
			catch(IOException ex)
			{
				_log.Error("Failed to write settings to file. " + ex.GetType() + " thrown with message: " + ex.Message);
				_log.Error("Stack Trace:\n" + ex.StackTrace);
			}
		}
	}
}
