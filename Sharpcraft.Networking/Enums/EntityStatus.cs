namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// Differet status codes sent by <see cref="Sharpcraft.Networking.Packets.EntityStatusPacket" /> (0x26).
	/// </summary>
	public enum EntityStatus
	{
		/// <summary>
		/// Entity hurt.
		/// </summary>
		EntityHurt	= 2,
		/// <summary>
		/// Entity dead/died.
		/// </summary>
		EntityDead	= 3,
		/// <summary>
		/// Wolf is being tamed.
		/// </summary>
		WolfTaming	= 6,
		/// <summary>
		/// Wolf has been tamed.
		/// </summary>
		WolfTamed	= 7,
		/// <summary>
		/// Wolf is shaking water off itself.
		/// </summary>
		WolfShake	= 8,
		/// <summary>
		/// Eating accepted by server.
		/// </summary>
		EatAccept	= 9
	}
}
