using System.IO;

using Newtonsoft.Json;

using Sharpcraft.Logging;

namespace Sharpcraft.Library.Configuration
{
	public class Settings
	{
		protected readonly log4net.ILog _log;

		public string SettingsFile { get; private set; }

		public Settings(string settingsFile)
		{
			_log = LogManager.GetLogger(this);

			SettingsFile = settingsFile;
		}

		public virtual void WriteToFile()
		{
			_log.Info("Saving settings to file...");
			var writer = new StreamWriter(SettingsFile, false, System.Text.Encoding.UTF8);
			var serializer = new JsonSerializer();
			serializer.Serialize(writer, this);
			writer.Close();
			_log.Info("Settings saved to file successfully!");
		}
	}
}
