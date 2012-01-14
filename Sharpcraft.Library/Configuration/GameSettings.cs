namespace Sharpcraft.Library.Configuration
{
	/// <summary>
	/// Settings related to the game (Sharpcraft).
	/// </summary>
	public class GameSettings : Settings
	{
		/// <summary>
		/// Initializes a new instance of <c>GameSettings</c>.
		/// </summary>
		/// <param name="settingsFile">The settings file to use.</param>
		public GameSettings(string settingsFile) : base(settingsFile) { }
	}
}
