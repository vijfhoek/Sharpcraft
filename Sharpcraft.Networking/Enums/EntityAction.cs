using Sharpcraft.Networking.Packets;

namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// The different actions sent by <see cref="EntityActionPacket" /> (0x13).
	/// </summary>
	public enum EntityAction
	{
		/// <summary>
		/// Crouch action.
		/// </summary>
		Crouch		= 1,
		/// <summary>
		/// Uncrouch action.
		/// </summary>
		Uncrouch	= 2,
		/// <summary>
		/// Leave bed action.
		/// </summary>
		LeaveBed	= 3,
		/// <summary>
		/// Start sprinting action.
		/// </summary>
		StartSprint	= 4,
		/// <summary>
		/// Stop sprinting action.
		/// </summary>
		StopSprint	= 5
	}
}
