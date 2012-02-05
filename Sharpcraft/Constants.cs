namespace Sharpcraft
{
	/// <summary>
	/// Contains various constants used by Sharpcraft.
	/// </summary>
	internal static class Constants
	{
		#region Content
		/// <summary>
		/// Directory where all game content is stored.
		/// </summary>
		internal const string ContentDirectory = "content";
		/// <summary>
		/// The directory where fonts are stored.
		/// </summary>
		/// <remarks>This is a subdirectory of <see cref="ContentDirectory" />.</remarks>
		internal const string FontDirectory = "fonts";
		/// <summary>
		/// Font used for the ingame menu.
		/// </summary>
		internal static string MenuFont { get { return FontDirectory + "\\font_menu"; } }
		/// <summary>
		/// Font used for the debugging text.
		/// </summary>
		internal static string DebugFont { get { return FontDirectory + "\\font_debug"; } }
		#endregion

		#region Configuration
		/// <summary>
		/// Directory where all settings are stored.
		/// </summary>
		internal const string SettingsDirectory = "settings";
		/// <summary>
		/// File used to store launcher settings.
		/// </summary>
		internal static string LauncherSettings { get { return SettingsDirectory + "\\launcher.settings"; } }
		/// <summary>
		/// File used to store game settings.
		/// </summary>
		internal static string GameSettings { get { return SettingsDirectory + "\\game.settings"; } }
		/// <summary>
		/// File containing info about the git commit from which this version was built.
		/// </summary>
		internal static string GitInfoFile { get { return SettingsDirectory + "\\gitinfo"; } }
		#endregion

		#region Data
		/// <summary>
		/// Directory where various data is stored.
		/// </summary>
		internal const string DataDirectory = "data";
		/// <summary>
		/// Directory where minecraft specific data is stored.
		/// </summary>
		internal static string MinecraftDataDirectory { get { return DataDirectory + "\\minecraft"; } }
		/// <summary>
		/// File containing block data.
		/// </summary>
		internal static string MinecraftBlockDataFile { get { return MinecraftDataDirectory + "\\blocks.list"; } }
		/// <summary>
		/// File containing item data.
		/// </summary>
		internal static string MinecraftItemDataFile { get { return MinecraftDataDirectory + "\\items.list"; } }
		#endregion
	}
}
