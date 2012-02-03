using System;
using System.Collections.Generic;

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
		
		public World(string seed = null, string type = null, sbyte dimension = 0, sbyte difficulty = 0, byte height = 0, Vector3 spawn = null)
		{
			Seed = seed;
			LevelType = type;
			Dimension = dimension;
			Difficulty = difficulty;
			Height = height;
			SetSpawn(spawn);
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
