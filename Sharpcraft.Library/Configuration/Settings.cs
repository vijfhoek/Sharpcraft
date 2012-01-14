using System.IO;

using Newtonsoft.Json;

using Sharpcraft.Logging;

namespace Sharpcraft.Library.Configuration
{
	/// <summary>
	/// Settings class used to store settings.
	/// </summary>
	internal class Settings
	{
		protected readonly log4net.ILog _log;

		/// <summary>
		/// The file used to store the settings of this settings class.
		/// </summary>
		private readonly string _settingsFile;

		/// <summary>
		/// Initialize a new instance of <c>Settings</c>.
		/// </summary>
		/// <param name="settingsFile">The settings file associated with this class.</param>
		protected Settings(string settingsFile)
		{
			_log = LogManager.GetLogger(this);

			_settingsFile = settingsFile;
		}

		/// <summary>
		/// Write settings to the settings file.
		/// </summary>
		public virtual void WriteToFile()
		{
			_log.Info("Saving settings to file...");
			var writer = new StreamWriter(_settingsFile, false, System.Text.Encoding.UTF8);
			var serializer = new JsonSerializer();
			serializer.Serialize(writer, this);
			writer.Close();
			_log.Info("Settings saved to file successfully!");
		}
	}
}
