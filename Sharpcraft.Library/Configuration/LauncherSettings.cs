/*
 * LauncherSettings.cs
 * 
 * Copyright © 2011-2012 by Sijmen Schoon and Adam Hellberg.
 * 
 * This file is part of Sharpcraft.
 * 
 * Sharpcraft is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Sharpcraft is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Sharpcraft.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Disclaimer: Sharpcraft is in no way affiliated with Mojang AB and/or
 * any of its employees and/or licensors.
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

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

		/// <summary>
		/// Write settings to the settings file.
		/// </summary>
		/// <remarks><see cref="LauncherSettings" /> overrides <see cref="Settings.WriteToFile" /> and logs any IO errors instead of crashing.</remarks>
		public override void WriteToFile()
		{
			try
			{
				base.WriteToFile();
			}
			catch(IOException ex)
			{
				Log.Error("Failed to write settings to file. " + ex.GetType() + " thrown with message: " + ex.Message);
				Log.Error("Stack Trace:\n" + ex.StackTrace);
			}
		}
	}
}
