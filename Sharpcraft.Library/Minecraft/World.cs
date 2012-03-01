/*
 * World.cs
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
 * Sijmen Schoon and Adam Hellberg does not take responsibility for
 * any harm caused, direct or indirect, to your Minecraft account
 * via the use of Sharpcraft.
 * 
 * "Minecraft" is a trademark of Mojang AB.
 */

using Microsoft.Xna.Framework;

namespace Sharpcraft.Library.Minecraft
{
	internal class World
	{
		public long Seed { get; private set; }
		public string LevelType { get; private set; }
		public Vector3 Spawn { get; private set; }
		public sbyte Dimension { get; private set; }
		public sbyte Difficulty { get; private set; }
		public byte Height { get; private set; }
		
		public World(long seed = 0, string type = null, sbyte dimension = 0, sbyte difficulty = 0, byte height = 0)
		{
			Seed = seed;
			LevelType = type;
			Dimension = dimension;
			Difficulty = difficulty;
			Height = height;
		}

		public void SetSeed(long seed)
		{
			Seed = seed;
		}

		public void SetLevelType(string type)
		{
			if (type != "DEFAULT" && type != "SUPERFLAT")
				LevelType = "DEFAULT";
			else
				LevelType = type;
		}

		public void SetDimension(sbyte dimension)
		{
			Dimension = dimension;
		}

		public void SetDifficulty(sbyte difficulty)
		{
			Difficulty = difficulty;
		}

		public void SetHeight(byte height)
		{
			Height = height;
		}

		public void SetSpawn(int x, int y, int z)
		{
			SetSpawn(new Vector3(x, y, z));
		}

		public void SetSpawn(Vector3 spawn)
		{
			Spawn = spawn;
		}
	}
}
