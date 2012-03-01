/*
 * Item.cs
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

namespace Sharpcraft.Library.Minecraft
{
	/// <summary>
	/// A minecraft item, this class only provides basic data.
	/// It does not contain information about textures et.c.
	/// </summary>
	public class Item
	{
		/// <summary>
		/// Name of the item.
		/// </summary>
		public string Name { get; private set; }
		
		/// <summary>
		/// ID of the item.
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// Data/Damage value of the item, 0 if none.
		/// </summary>
		public int Data { get; private set; }

		/// <summary>
		/// Whether or not this item can be damaged.
		/// </summary>
		public bool Damageable { get; private set; }

		/// <summary>
		/// Initialize a new instance of <see cref="Item" />.
		/// </summary>
		/// <param name="name">Name of the item.</param>
		/// <param name="id">ID of the item.</param>
		/// <param name="data">Data/Damage value of the item, defaults to zero (0).</param>
		/// <param name="damageable">Wether the item degrades whilst it is being used, defaults to false.</param>
		public Item(string name, int id, int data = 0, bool damageable = false)
		{
			Name = name;
			Id = id;
			Data = data;
			Damageable = damageable;
		}
	}
}
