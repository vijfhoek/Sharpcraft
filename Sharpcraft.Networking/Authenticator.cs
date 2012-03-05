/*
 * Authenticator.cs
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
 * Sijmen Schoon and Adam Hellberg do not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using System.IO;
using System.Net;
using System.Text;
using Sharpcraft.Logging;

namespace Sharpcraft.Networking
{
	/// <summary>
	/// Login result returned by <see cref="Authenticator" />.Login method,
	/// can be used to determine if login was successful and display the error
	/// message if it was not.
	/// </summary>
	public struct LoginResult
	{
		/// <summary>
		/// Whether or not the login succeeded.
		/// </summary>
		public readonly bool Success;

		/// <summary>
		/// The result of the login,
		/// this will contain an error message if it was unsuccessful.
		/// </summary>
		public readonly string Result;

		/// <summary>
		/// The user's real username.
		/// </summary>
		public readonly string RealName;

		/// <summary>
		/// The session ID associated with this login.
		/// </summary>
		public readonly string SessionID;

		/// <summary>
		/// Initializes a new instance of the <c>LoginResult</c> struct.
		/// </summary>
		/// <param name="success">Whether or not the login succeeded.</param>
		/// <param name="result">The result string, as returned from the minecraft servers.</param>
		/// <param name="realName">The user's real username.</param>
		/// <param name="sessionId">The user's session ID.</param>
		internal LoginResult(bool success, string result, string realName = null, string sessionId = null)
		{
			Success = success;
			Result = result;
			RealName = realName;
			SessionID = sessionId;
		}
	}

	/// <summary>
	/// Class containing methods for authenticating a minecraft user
	/// and logging in via minecraft.net.
	/// </summary>
	public class Authenticator
	{
		/// <summary>
		/// Log object for this class.
		/// </summary>
		private readonly log4net.ILog _log;

		/// <summary>
		/// Address to send login requests to.
		/// </summary>
		private const string AuthAddress = "https://login.minecraft.net/";

		/// <summary>
		/// The format to use for POST data. Passed to <see cref="string" />.Format.
		/// </summary>
		private const string AuthData = "user={0}&password={1}&version={2}";

		/// <summary>
		/// Minecraft Version.
		/// </summary>
		private readonly int _version;

		/// <summary>
		/// Initialize a new instance of the <see cref="Authenticator" /> class.
		/// </summary>
		/// <param name="version">Minecraft Version.</param>
		public Authenticator(int version)
		{
			_log = LogManager.GetLogger(this);
			_version = version;
		}

		/// <summary>
		/// The login event, fires when login succeeds or fails.
		/// </summary>
		public event LoginEventHandler OnLoginEvent;

		/// <summary>
		/// Fires the login event.
		/// </summary>
		/// <param name="e">The <see cref="LoginEventArgs" /> for the event.</param>
		private void LoginEvent(LoginEventArgs e)
		{
			if (OnLoginEvent != null)
				OnLoginEvent(e);
		}

		/// <summary>
		/// Sends a login request to the minecraft authentication server
		/// and returns the response from it.
		/// </summary>
		/// <param name="username">Minecraft Username</param>
		/// <param name="password">Minecraft Password</param>
		/// <returns><see cref="LoginResult" /> struct containing details about the result.</returns>
		public LoginResult Login(string username, string password)
		{
			string data = string.Format(AuthData, username, password, _version);

			// Create HTTPS POST request
			_log.Debug("Creating HTTPS request...");
			var request = (HttpWebRequest) WebRequest.Create(AuthAddress);
			request.Method = "POST";
			request.KeepAlive = false;
			request.ProtocolVersion = HttpVersion.Version11;

			// Turn string data into byte array
			byte[] postData = Encoding.ASCII.GetBytes(data);

			// Specify type
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = postData.Length;

			// Get stream
			Stream stream;
			try
			{
				_log.Debug("Getting request stream...");
				stream = request.GetRequestStream();
			}
			catch(WebException ex)
			{
				_log.Warn("Failed to contact " + AuthAddress + ". " + ex.GetType() + ": " + ex.Message);
				var result = new LoginResult(false, "Failed to contact " + AuthAddress + ". "
													+ ex.GetType() + " thrown with message: " + ex.Message);
				LoginEvent(new LoginEventArgs(result));
				return result;
			}

			// Send request
			_log.Info("Sending HTTPS POST request to " + AuthAddress);
			stream.Write(postData, 0, postData.Length);
			stream.Close();

			// Get response
			HttpWebResponse response;
			try
			{
				_log.Debug("Retrieving response from request...");
				response = (HttpWebResponse) request.GetResponse();
			}
			catch (WebException ex)
			{
				_log.Warn("Failed to retrieve response from " + AuthAddress + ". " + ex.GetType() + ": " + ex.Message);
				var result = new LoginResult(false, "Failed to retrieve response from " + AuthAddress + ". "
											+ ex.GetType() + " thrown with message: " + ex.Message);
				LoginEvent(new LoginEventArgs(result));
				return result;
			}

			_log.Debug("Creating response stream...");
			Stream responseStream = response.GetResponseStream();
			if (responseStream == null)
			{
				_log.Warn("Response stream was NULL. Login failed.");
				var result = new LoginResult(false, "Response stream was NULL. Login failed.");
				LoginEvent(new LoginEventArgs(result));
				return result;
			}

			_log.Debug("Creating response reader...");
			var reader = new StreamReader(responseStream);
			string responseString = reader.ReadToEnd();

			_log.Info("Got response: " + responseString);

			string[] responseArray = responseString.Split(':');

			if (responseArray.Length < 4)
			{
				_log.Warn("Incorrect response format, expected 4 fields of data, got " + responseArray.Length);
				_log.Warn("Login failed.");
				var result = new LoginResult(false, "Invalid response data size, expected 4, got " + responseArray.Length +
											".\nResponse was:\n" + responseString);
				LoginEvent(new LoginEventArgs(result));
				return result;
			}

			LoginEvent(new LoginEventArgs(new LoginResult(true, responseString, responseArray[2], responseArray[3])));
			return new LoginResult(true, responseString, responseArray[2], responseArray[3]);
		}
	}
}
