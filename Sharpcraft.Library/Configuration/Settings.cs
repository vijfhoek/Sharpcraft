/*
 * Settings.cs
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

using Newtonsoft.Json;

using Sharpcraft.Logging;

namespace Sharpcraft.Library.Configuration
{
	/// <summary>
	/// Settings class used to store settings.
	/// </summary>
	public class Settings
	{
		/// <summary>
		/// The log object associated with this class.
		/// </summary>
		protected readonly log4net.ILog Log;

		/// <summary>
		/// The file used to store the settings of this settings class.
		/// </summary>
		[JsonProperty("SettingsFile")]
		private readonly string _settingsFile;

		/// <summary>
		/// Initialize a new instance of <c>Settings</c>.
		/// </summary>
		/// <param name="settingsFile">The settings file associated with this class.</param>
		protected Settings(string settingsFile)
		{
			Log = LogManager.GetLogger(this);

			_settingsFile = settingsFile;
		}

		/// <summary>
		/// Write settings to the settings file.
		/// </summary>
		public virtual void WriteToFile()
		{
			Log.Info("Saving settings to file...");
			var writer = new StreamWriter(_settingsFile, false, System.Text.Encoding.UTF8);
			var serializer = new JsonSerializer();
			serializer.Serialize(new JsonTextWriter(writer) {Formatting = Formatting.Indented}, this);
			writer.Close();
			Log.Info("Settings saved to file successfully!");
		}
	}
}
