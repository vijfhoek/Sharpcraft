namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// Direction of a piston push sent by <see cref="Sharpcraft.Networking.Packets.BlockActionPacket"/> (0x36).
	/// </summary>
	public enum BlockActionDirection
	{
		/// <summary>
		/// Down direction.
		/// </summary>
		Down	= 0,
		/// <summary>
		/// Up direction.
		/// </summary>
		Up		= 1,
		/// <summary>
		/// South direction.
		/// </summary>
		South	= 2,
		/// <summary>
		/// West direction.
		/// </summary>
		West	= 3,
		/// <summary>
		/// North direction.
		/// </summary>
		North	= 4,
		/// <summary>
		/// East direction.
		/// </summary>
		East	= 5
	}
}
