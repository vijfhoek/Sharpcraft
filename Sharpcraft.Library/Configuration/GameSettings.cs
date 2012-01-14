using System;
using System.IO;

using Newtonsoft.Json;

namespace Sharpcraft.Library.Configuration
{
	class GameSettings : Settings
	{
		public GameSettings(string settingsFile) : base(settingsFile)
		{
			/*
			if (File.Exists(SettingsFile))
			{
				_log.Info("Loading " + this + " from file...");
				var reader = new StreamReader(SettingsFile, System.Text.Encoding.UTF8);
				var settings = new JsonSerializer().Deserialize<GameSettings>(new JsonTextReader(reader));
				_log.Info(this + " loaded successfully!");
			}
			*/
		}
	}
}
