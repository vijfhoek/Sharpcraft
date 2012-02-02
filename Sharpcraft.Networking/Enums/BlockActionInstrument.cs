namespace Sharpcraft.Networking.Enums
{
	/// <summary>
	/// Different instrument type for note block sent by <see cref="Sharpcraft.Networking.Packets.BlockActionPacket" /> (0x36).
	/// </summary>
	public enum BlockActionInstrument
	{
		/// <summary>
		/// The harp.
		/// </summary>
		Harp			= 0,
		/// <summary>
		/// The double bass.
		/// </summary>
		DoubleBass		= 1,
		/// <summary>
		/// The snare drum.
		/// </summary>
		SnareDrum		= 2,
		/// <summary>
		/// The clicks/sticks.
		/// </summary>
		ClicksSticks	= 3,
		/// <summary>
		/// The bass drum.
		/// </summary>
		BassDrum		= 4
	}
}
