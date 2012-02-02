namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// The different animation types sent by the Animation packet (0x12).
	/// </summary>
	public enum Animation
	{
		/// <summary>
		/// No animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		NoAnimation		= 0,
		/// <summary>
		/// Swing arm (e.g. when attacking).
		/// </summary>
		SwingArm		= 1,
		/// <summary>
		/// Damage animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		DamageAnimation	= 2,
		/// <summary>
		/// Leave bed animation.
		/// </summary>
		LeaveBed		= 3,
		/// <summary>
		/// Eat food animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		EatFood			= 5,
		/// <summary>
		/// Unknown.
		/// </summary>
		Unknown			= 102,
		/// <summary>
		/// Crouch animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		Crouch			= 104,
		/// <summary>
		/// Uncrouch animation.
		/// </summary>
		/// <remarks>Not sent by notchian clients.</remarks>
		Uncrouch		= 105
	}
}
