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
