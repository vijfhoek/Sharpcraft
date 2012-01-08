using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using Sharpcraft.Logging;

namespace Sharpcraft.Networking
{
	public class MinecraftNet
	{
		private readonly log4net.ILog _log;

		public MinecraftNet()
		{
			_log = LogManager.GetLogger(this);
		}

		public bool Login(string username, string password, int version)
		{
			_log.Info("Sending HTTPS POST request to https://login.minecraft.net/");
			string postData = "user=" + username + "&password=" + password + "&version=" + version;
			string url = "https://login.minecraft.net/";

			// Create a HTTPS POST request
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.KeepAlive = false;
			request.ProtocolVersion = HttpVersion.Version11;
			request.Method = "POST";

			// Turn our post data into a byte array
			byte[] postBytes = Encoding.ASCII.GetBytes(postData);

			// Specify type
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = postBytes.Length;
			Stream requestStream = request.GetRequestStream();

			// Send the request
			requestStream.Write(postBytes, 0, postBytes.Length);
			requestStream.Close();

			// Get the response
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader responseStreamReader = new StreamReader(responseStream);
			String responseString = responseStreamReader.ReadToEnd();

			_log.Info("Got response: " + responseString);

			// Return true for now
			return true;
		}
	}
}
