namespace Sharpcraft
{
	internal static class SharpcraftConstants
	{
		#region Content
		internal const string ContentDirectory = "content";
		internal const string FontDirectory = "fonts";
		internal static string MenuFont { get { return FontDirectory + "\\font_menu"; } }
		internal static string DebugFont { get { return FontDirectory + "\\font_debug"; } }
		#endregion

		#region Configuration
		internal const string SettingsDirectory = "settings";
		internal static string LauncherSettings { get { return SettingsDirectory + "\\launcher.settings"; } }
		internal static string GameSettings { get { return SettingsDirectory + "\\game.settings"; } }
		#endregion
	}
}
