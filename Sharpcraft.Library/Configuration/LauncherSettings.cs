using System;
using System.IO;
using System.Text;
using System.Linq;

using Newtonsoft.Json;

namespace Sharpcraft.Library.Configuration
{
	public class LauncherSettings : Settings
	{
		[JsonIgnore]
		private const string Key = "~L}%JiYK3&Vq]ezC";

		public string Username;

		[JsonProperty]
		private string _password;

		public bool Remember;

		public LauncherSettings(string settingsFile) : base(settingsFile) { }

		public void SetPassword(string password)
		{
			var pass = Encoding.UTF8.GetBytes(password);
			var data = pass.Select((c, i) => (byte) (c ^ (i % Key.Length))).ToArray();
			_password = Convert.ToBase64String(data);
		}

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
