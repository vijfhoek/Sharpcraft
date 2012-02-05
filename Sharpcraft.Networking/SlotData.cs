using LibNbt.Tags;

// Temporarily disabled the notification that there is no docs
// ReSharper disable CSharpWarnings::CS1591

namespace Sharpcraft.Networking
{
	public class SlotData
	{
		public short ItemID;
		public byte ItemCount;
		public short ItemDamage;
		public NbtList ItemEnchantments;

		public SlotData(short itemID = -1, byte itemCount = 0, short itemDamage = 0, NbtList itemEnchantments = null)
		{
			ItemID = itemID;
			ItemCount = itemCount;
			ItemDamage = itemDamage;
			ItemEnchantments = itemEnchantments;
		}
	}
}

// ReSharper restore CSharpWarnings::CS1591