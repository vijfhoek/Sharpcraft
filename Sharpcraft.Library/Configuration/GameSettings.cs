using System;
using System.IO;

using Newtonsoft.Json;

namespace Sharpcraft.Library.Configuration
{
	class GameSettings : Settings
	{
		public GameSettings(string settingsFile) : base(settingsFile)
		{
			
		}
	}
}
