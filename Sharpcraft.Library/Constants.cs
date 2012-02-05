namespace Sharpcraft.Library
{
	public static class Constants
	{
		public const string DataDirectory = "data";
		public static string MinecraftDataDirectory { get { return DataDirectory + "\\minecraft"; } }
		public static string MinecraftItemFile { get { return MinecraftDataDirectory + "\\items.list"; } }
	}
}
