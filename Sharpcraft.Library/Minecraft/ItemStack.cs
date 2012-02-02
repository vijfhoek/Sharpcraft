using LibNbt.Tags;

namespace Sharpcraft.Library.Minecraft
{
	/// <summary>
	/// Contains information about a stack of items.
	/// </summary>
	public class ItemStack
	{
		/// <summary>
		/// Contains how many items there are in the stack.
		/// </summary>
		public int StackSize;

		// TODO figure out what this does
		/// <summary>
		/// Yet to figure out what this does.
		/// </summary>
		public int AnimationsToGo;

		/// <summary>
		/// The Item this stack contains.
		/// </summary>
		public Item ItemID;

		/// <summary>
		/// The damage values of the stack.
		/// </summary>
		public int ItemDamage;

		/// <summary>
		/// Will contain an NBT compound.
		/// </summary>
		public NbtCompound StackTagCompound;
		
		/// <summary>
		/// Initializes the ItemStack.
		/// </summary>
		/// <param name="itemID">The item ID, defaults to 0</param>
		/// <param name="stackSize">The stack size, defaults to 0</param>
		/// <param name="itemDamage">The item damage, defaults to 0</param>
		public ItemStack(int itemID = 0, int stackSize = 0, int itemDamage = 0)
		{
			ItemID = itemID;
			StackSize = stackSize;
			ItemDamage = itemDamage;
		}
	}
}
